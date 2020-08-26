using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Refunds;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderRefundEventHandler : IDistributedEventHandler<EntityCreatedEto<EShopRefundEto>>, ITransientDependency
    {
        private readonly IOrderRepository _orderRepository;

        public OrderRefundEventHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityCreatedEto<EShopRefundEto> eventData)
        {
            foreach (var refundItem in eventData.Entity.RefundItems)
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