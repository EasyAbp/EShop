using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
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

    public CreateFlashSaleOrderEventHandler(
        INewOrderGenerator newOrderGenerator,
        IObjectMapper objectMapper,
        IEnumerable<IOrderDiscountProvider> orderDiscountProviders,
        IOrderRepository orderRepository,
        IDistributedEventBus distributedEventBus)
    {
        NewOrderGenerator = newOrderGenerator;
        ObjectMapper = objectMapper;
        OrderDiscountProviders = orderDiscountProviders;
        OrderRepository = orderRepository;
        DistributedEventBus = distributedEventBus;
    }

    [UnitOfWork(true)]
    public virtual async Task HandleEventAsync(CreateFlashSaleOrderEto eventData)
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
        var productDict = new Dictionary<Guid, ProductDto>()
        {
            {eventData.Product.Id,  ObjectMapper.Map<FlashSaleProductEto,ProductDto>(eventData.Product)}
        };
        var productDetailDict = new Dictionary<Guid, ProductDetailDto>()
        {
            {eventData.ProductDetail.Id, ObjectMapper.Map<FlashSaleProductDetailEto,ProductDetailDto>(eventData.ProductDetail)}
        };

        var order = await NewOrderGenerator.GenerateAsync(eventData.UserId, input, productDict, productDetailDict);

        await DiscountOrderAsync(order, productDict);

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

    protected virtual async Task DiscountOrderAsync(Order order, Dictionary<Guid, ProductDto> productDict)
    {
        foreach (var provider in OrderDiscountProviders)
        {
            await provider.DiscountAsync(order, productDict);
        }
    }
}
