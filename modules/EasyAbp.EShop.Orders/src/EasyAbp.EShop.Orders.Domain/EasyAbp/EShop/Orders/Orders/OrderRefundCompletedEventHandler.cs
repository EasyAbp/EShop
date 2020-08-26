using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Refunds;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderRefundCompletedEventHandler : IDistributedEventHandler<OrderRefundCompletedEto>, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IOrderRepository _orderRepository;

        public OrderRefundCompletedEventHandler(
            ICurrentTenant currentTenant,
            IOrderRepository orderRepository)
        {
            _currentTenant = currentTenant;
            _orderRepository = orderRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(OrderRefundCompletedEto eventData)
        {
            using var changeTenant = _currentTenant.Change(eventData.Refund.TenantId);

            foreach (var refundItem in eventData.Refund.RefundItems)
            {
                var order = await _orderRepository.GetAsync(refundItem.OrderId);

                foreach (var eto in refundItem.RefundItemOrderLines)
                {
                    order.Refund(eto.OrderLineId, eto.RefundedQuantity, eto.RefundAmount);
                }

                await _orderRepository.UpdateAsync(order, true);
            }
        }
    }
}