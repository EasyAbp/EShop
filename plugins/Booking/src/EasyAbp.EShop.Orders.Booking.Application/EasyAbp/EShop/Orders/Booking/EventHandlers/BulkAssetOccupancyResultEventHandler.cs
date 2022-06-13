using System.Threading.Tasks;
using EasyAbp.BookingService.AssetOccupancies;
using EasyAbp.BookingService.AssetOccupancyProviders;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Payments.Refunds;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Booking.EventHandlers;

public class BulkAssetOccupancyResultEventHandler : IDistributedEventHandler<BulkAssetOccupancyResultEto>, ITransientDependency
{
    private readonly IOrderManager _orderManager;
    private readonly IOrderRepository _orderRepository;
    private readonly IDistributedEventBus _distributedEventBus;

    public BulkAssetOccupancyResultEventHandler(
        IOrderManager orderManager,
        IOrderRepository orderRepository,
        IDistributedEventBus distributedEventBus)
    {
        _orderManager = orderManager;
        _orderRepository = orderRepository;
        _distributedEventBus = distributedEventBus;
    }

    [UnitOfWork(true)]
    public virtual async Task HandleEventAsync(BulkAssetOccupancyResultEto eventData)
    {
        var orderId = eventData.FindBookingOrderId();
            
        if (orderId is null)
        {
            return;
        }

        var order = await _orderRepository.GetAsync(orderId.Value);
        
        if (eventData.Success)
        {
            if (order.CompletionTime.HasValue)
            {
                return;
            }
            
            await _orderManager.CompleteAsync(order);
        }
        else
        {
            if (order.CanceledTime.HasValue)
            {
                return;
            }
            
            await _orderManager.CancelAsync(order, BookingOrderConsts.BookingOrderAutoCancellationResult);
            
            await TryRefundOrderAsync(order);
        }
    }

    protected virtual async Task TryRefundOrderAsync(Order order)
    {
        if (order.PaymentId is null)
        {
            return;
        }
        
        var eto = new RefundOrderEto(
            order.TenantId,
            order.Id,
            order.StoreId,
            order.PaymentId.Value,
            BookingOrderConsts.BookingOrderAutoCancellationResult,
            BookingOrderConsts.BookingOrderAutoCancellationResult,
            BookingOrderConsts.BookingOrderAutoCancellationResult);

        await _distributedEventBus.PublishAsync(eto);
    }
}