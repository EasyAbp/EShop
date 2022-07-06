using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Products.ProductDetails;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.Products;

public class FlashSaleReduceInventoryEventHandler : IDistributedEventHandler<FlashSaleReduceInventoryEto>, ITransientDependency
{
    protected IProductRepository ProductRepository { get; }

    protected IProductDetailRepository ProductDetailRepository { get; }

    protected ProductManager ProductManager { get; }

    protected IDistributedEventBus DistributedEventBus { get; }

    protected IObjectMapper ObjectMapper { get; }

    protected IFlashSalePlanHasher FlashSalePlanHasher { get; }

    public FlashSaleReduceInventoryEventHandler(
        IProductRepository productRepository,
        IProductDetailRepository productDetailRepository,
        ProductManager productManager,
        IDistributedEventBus distributedEventBus,
        IObjectMapper objectMapper,
        IFlashSalePlanHasher flashSalePlanHasher)
    {
        ProductRepository = productRepository;
        ProductDetailRepository = productDetailRepository;
        ProductManager = productManager;
        DistributedEventBus = distributedEventBus;
        ObjectMapper = objectMapper;
        FlashSalePlanHasher = flashSalePlanHasher;
    }

    [UnitOfWork]
    public virtual async Task HandleEventAsync(FlashSaleReduceInventoryEto eventData)
    {
        var product = await ProductRepository.GetAsync(eventData.Plan.ProductId);
        var productSku = product.ProductSkus.Single(x => x.Id == eventData.Plan.ProductSkuId);

        var hashToken = await FlashSalePlanHasher.HashAsync(eventData.Plan.LastModificationTime, product.LastModificationTime, productSku.LastModificationTime);

        if (hashToken != eventData.HashToken)
        {
            await DistributedEventBus.PublishAsync(new CreateFlashSaleOrderCompleteEto()
            {
                TenantId = eventData.TenantId,
                PlanId = eventData.PlanId,
                OrderId = null,
                UserId = eventData.UserId,
                StoreId = eventData.StoreId,
                PendingResultId = eventData.PendingResultId,
                Success = false,
                Reason = FlashSaleResultFailedReason.PreOrderExipred
            });
            return;
        }

        if (!await ProductManager.TryReduceInventoryAsync(product, productSku, 1, true))
        {
            await DistributedEventBus.PublishAsync(new CreateFlashSaleOrderCompleteEto()
            {
                TenantId = eventData.TenantId,
                PlanId = eventData.PlanId,
                OrderId = null,
                UserId = eventData.UserId,
                StoreId = eventData.StoreId,
                PendingResultId = eventData.PendingResultId,
                Success = false,
                Reason = FlashSaleResultFailedReason.InsufficientInventory
            });
            return;
        }

        var productDetailId = productSku.ProductDetailId ?? product.ProductDetailId;

        var productDetailEto = productDetailId.HasValue ?
             ObjectMapper.Map<ProductDetail, FlashSaleProductDetailEto>(await ProductDetailRepository.GetAsync(productDetailId.Value)) :
             null;

        var productEto = ObjectMapper.Map<Product, FlashSaleProductEto>(product);

        var createFlashSalesOrderEto = new CreateFlashSaleOrderEto()
        {
            TenantId = eventData.TenantId,
            PlanId = eventData.PlanId,
            UserId = eventData.UserId,
            PendingResultId = eventData.PendingResultId,
            StoreId = eventData.StoreId,
            CreateTime = eventData.CreateTime,
            CustomerRemark = eventData.CustomerRemark,
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
