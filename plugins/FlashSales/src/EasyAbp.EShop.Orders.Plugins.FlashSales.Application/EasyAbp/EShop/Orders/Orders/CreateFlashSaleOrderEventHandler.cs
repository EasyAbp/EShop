using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders;

public class CreateFlashSaleOrderEventHandler : IDistributedEventHandler<CreateFlashSaleOrderEto>, ITransientDependency
{
    protected INewOrderGenerator NewOrderGenerator { get; }

    protected IObjectMapper ObjectMapper { get; }

    protected IEnumerable<IOrderDiscountProvider> OrderDiscountProviders { get; }

    protected IOrderRepository OrderRepository { get; }

    protected IDistributedEventBus DistributedEventBus { get; }

    protected IProductAppService ProductAppService { get; }

    protected IProductDetailAppService ProductDetailAppService { get; }

    protected IFlashSalePlanHasher FlashSalePlanHasher { get; }

    public CreateFlashSaleOrderEventHandler(
        INewOrderGenerator newOrderGenerator,
        IObjectMapper objectMapper,
        IEnumerable<IOrderDiscountProvider> orderDiscountProviders,
        IOrderRepository orderRepository,
        IDistributedEventBus distributedEventBus,
        IProductAppService productAppService,
        IProductDetailAppService productDetailAppService,
        IFlashSalePlanHasher flashSalePlanHasher)
    {
        NewOrderGenerator = newOrderGenerator;
        ObjectMapper = objectMapper;
        OrderDiscountProviders = orderDiscountProviders;
        OrderRepository = orderRepository;
        DistributedEventBus = distributedEventBus;
        ProductAppService = productAppService;
        ProductDetailAppService = productDetailAppService;
        FlashSalePlanHasher = flashSalePlanHasher;
    }

    [UnitOfWork(true)]
    public virtual async Task HandleEventAsync(CreateFlashSaleOrderEto eventData)
    {
        var product = await ProductAppService.GetAsync(eventData.Plan.ProductId);
        var productSku = product.GetSkuById(eventData.Plan.ProductSkuId);

        if (!await ValidateHashTokenAsync(eventData.Plan, product, productSku, eventData.HashToken))
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

        var input = await ConvertToCreateOrderDtoAsync(eventData);

        var productDict = await GetProductDictionaryAsync(product);

        var productDetailDict = await GetProductDetailDictionaryAsync(product, productSku);

        var order = await NewOrderGenerator.GenerateAsync(eventData.UserId, input, productDict, productDetailDict);

        await OrderRepository.InsertAsync(order, autoSave: true);

        await DistributedEventBus.PublishAsync(new CreateFlashSaleOrderCompleteEto()
        {
            TenantId = eventData.TenantId,
            PlanId = eventData.PlanId,
            OrderId = order.Id,
            UserId = eventData.UserId,
            StoreId = eventData.StoreId,
            PendingResultId = eventData.PendingResultId,
            Success = true
        });
    }

    protected virtual Task<Dictionary<Guid, ProductDto>> GetProductDictionaryAsync(ProductDto product)
    {
        var productDict = new Dictionary<Guid, ProductDto>()
        {
            {product.Id, product}
        };

        return Task.FromResult(productDict);
    }

    protected virtual async Task<Dictionary<Guid, ProductDetailDto>> GetProductDetailDictionaryAsync(ProductDto product, ProductSkuDto productSku)
    {
        var dict = new Dictionary<Guid, ProductDetailDto>();

        var productDetailId = productSku.ProductDetailId ?? product.ProductDetailId;

        if (productDetailId.HasValue)
        {
            dict.Add(productDetailId.Value, await ProductDetailAppService.GetAsync(productDetailId.Value));
        }

        return dict;
    }

    protected virtual Task<CreateOrderDto> ConvertToCreateOrderDtoAsync(CreateFlashSaleOrderEto eventData)
    {
        var input = new CreateOrderDto()
        {
            StoreId = eventData.StoreId,
            CustomerRemark = eventData.CustomerRemark,
            OrderLines = new List<CreateOrderLineDto>()
            {
                new CreateOrderLineDto()
                {
                    ProductId = eventData.Plan.ProductId,
                    ProductSkuId = eventData.Plan.ProductSkuId,
                    Quantity = 1
                }
            }
        };

        return Task.FromResult(input);
    }

    protected virtual async Task<bool> ValidateHashTokenAsync(FlashSalePlanEto plan, ProductDto product, ProductSkuDto productSku, string originHashToken)
    {
        var hashToken = await FlashSalePlanHasher.HashAsync(plan.LastModificationTime, product.LastModificationTime, productSku.LastModificationTime);

        return string.Equals(hashToken, originHashToken, StringComparison.InvariantCulture);
    }
}
