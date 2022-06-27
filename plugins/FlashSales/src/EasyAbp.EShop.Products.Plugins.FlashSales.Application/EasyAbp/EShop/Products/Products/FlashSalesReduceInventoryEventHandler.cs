using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;
using EasyAbp.EShop.Products.ProductDetails;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.Products;

public class FlashSalesReduceInventoryEventHandler : IDistributedEventHandler<FlashSalesReduceInventoryEto>, ITransientDependency
{
    protected IProductRepository ProductRepository { get; }

    protected IProductDetailRepository ProductDetailRepository { get; }

    protected ProductManager ProductManager { get; }

    protected IDistributedEventBus DistributedEventBus { get; }

    protected IObjectMapper ObjectMapper { get; }

    protected IFlashSalesPlanHasher FlashSalesPlanHasher { get; }

    public FlashSalesReduceInventoryEventHandler(
        IProductRepository productRepository,
        IProductDetailRepository productDetailRepository,
        ProductManager productManager,
        IDistributedEventBus distributedEventBus,
        IObjectMapper objectMapper,
        IFlashSalesPlanHasher flashSalesPlanHasher)
    {
        ProductRepository = productRepository;
        ProductDetailRepository = productDetailRepository;
        ProductManager = productManager;
        DistributedEventBus = distributedEventBus;
        ObjectMapper = objectMapper;
        FlashSalesPlanHasher = flashSalesPlanHasher;
    }

    [UnitOfWork]
    public virtual async Task HandleEventAsync(FlashSalesReduceInventoryEto eventData)
    {
        var product = await ProductRepository.GetAsync(eventData.Plan.ProductId);
        var productSku = product.ProductSkus.Single(x => x.Id == eventData.Plan.ProductSkuId);

        var hashToken = await FlashSalesPlanHasher.HashAsync(eventData.Plan.LastModificationTime, product.LastModificationTime, productSku.LastModificationTime);

        if (hashToken != eventData.HashToken)
        {
            await DistributedEventBus.PublishAsync(new CreateFlashSalesOrderCompleteEto()
            {
                TenantId = eventData.TenantId,
                PlanId = eventData.PlanId,
                OrderId = null,
                UserId = eventData.UserId,
                StoreId = eventData.StoreId,
                PendingResultId = eventData.PendingResultId,
                Success = false,
                Reason = FlashSalesResultFailedReason.PreOrderExipred
            });
            return;
        }

        if (!await ProductManager.TryReduceInventoryAsync(product, productSku, eventData.Quantity, true))
        {
            await DistributedEventBus.PublishAsync(new CreateFlashSalesOrderCompleteEto()
            {
                TenantId = eventData.TenantId,
                PlanId = eventData.PlanId,
                OrderId = null,
                UserId = eventData.UserId,
                StoreId = eventData.StoreId,
                PendingResultId = eventData.PendingResultId,
                Success = false,
                Reason = FlashSalesResultFailedReason.InsufficientInventory
            });
            return;
        }

        var productDetailId = productSku.ProductDetailId ?? product.ProductDetailId;

        var productDetailEto = productDetailId.HasValue ?
             ObjectMapper.Map<ProductDetail, FlashSalesProductDetailEto>(await ProductDetailRepository.GetAsync(productDetailId.Value)) :
             null;

        var productEto = ObjectMapper.Map<Product, FlashSalesProductEto>(product);

        var createFlashSalesOrderEto = new CreateFlashSalesOrderEto()
        {
            TenantId = eventData.TenantId,
            PlanId = eventData.PlanId,
            UserId = eventData.UserId,
            PendingResultId = eventData.PendingResultId,
            StoreId = eventData.StoreId,
            CreateTime = eventData.CreateTime,
            CustomerRemark = eventData.CustomerRemark,
            Quantity = eventData.Quantity,
            Product = productEto,
            ProductDetail = productDetailEto,
            Plan = eventData.Plan
        };

        foreach (var item in eventData.ExtraProperties)
        {
            createFlashSalesOrderEto.ExtraProperties.Add(item.Key, item.Value);
        }

        await DistributedEventBus.PublishAsync(createFlashSalesOrderEto);
    }
}
