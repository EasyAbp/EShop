using System.Threading.Tasks;
using EasyAbp.BookingService.AssetOccupancies;
using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Booking.EventHandlers;

public class BulkAssetOccupancyResultEventHandler : IDistributedEventHandler<BulkAssetOccupancyResultEto>, ITransientDependency
{
    private readonly IOrderManager _orderManager;
    private readonly IOrderRepository _orderRepository;

    public BulkAssetOccupancyResultEventHandler(
        IOrderManager orderManager,
        IOrderRepository orderRepository)
    {
        _orderManager = orderManager;
        _orderRepository = orderRepository;
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
            
            await RefundOrderAsync(order);
        }
    }

    protected virtual async Task RefundOrderAsync(Order order)
    {
        // Todo: create a RefundOrderPaymentEto event to refund the order's payment.
    }
}