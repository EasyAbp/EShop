using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Refunds;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class RefundCompletedEventHandler : IDistributedEventHandler<EShopRefundCompletedEto>, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IObjectMapper _objectMapper;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IOrderRepository _orderRepository;

        public RefundCompletedEventHandler(
            ICurrentTenant currentTenant,
            IObjectMapper<EShopOrdersDomainModule> objectMapper,
            IDistributedEventBus distributedEventBus,
            IOrderRepository orderRepository)
        {
            _currentTenant = currentTenant;
            _objectMapper = objectMapper;
            _distributedEventBus = distributedEventBus;
            _orderRepository = orderRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EShopRefundCompletedEto eventData)
        {
            using var changeTenant = _currentTenant.Change(eventData.Refund.TenantId);

            foreach (var refundItem in eventData.Refund.RefundItems)
            {
                // Todo: what if Order.PaymentId != eventData.Refund.PaymentId?
                var order = await _orderRepository.GetAsync(refundItem.OrderId);

                foreach (var eto in refundItem.OrderLines)
                {
                    order.RefundOrderLine(eto.OrderLineId, eto.RefundedQuantity, eto.RefundAmount);
                }

                foreach (var eto in refundItem.OrderExtraFees)
                {
                    order.RefundOrderExtraFee(eto.Name, eto.Key, eto.RefundAmount);
                }

                await _orderRepository.UpdateAsync(order, true);

                await _distributedEventBus.PublishAsync(
                    new OrderRefundedEto(_objectMapper.Map<Order, OrderEto>(order), eventData.Refund));
            }
        }
    }
}