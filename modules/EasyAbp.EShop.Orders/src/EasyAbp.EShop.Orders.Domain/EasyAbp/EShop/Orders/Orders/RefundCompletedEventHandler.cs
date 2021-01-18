using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Refunds;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
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
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IOrderRepository _orderRepository;

        public RefundCompletedEventHandler(
            ICurrentTenant currentTenant,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWorkManager,
            IDistributedEventBus distributedEventBus,
            IOrderRepository orderRepository)
        {
            _currentTenant = currentTenant;
            _objectMapper = objectMapper;
            _unitOfWorkManager = unitOfWorkManager;
            _distributedEventBus = distributedEventBus;
            _orderRepository = orderRepository;
        }
        
        public virtual async Task HandleEventAsync(EShopRefundCompletedEto eventData)
        {
            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            
            using var changeTenant = _currentTenant.Change(eventData.Refund.TenantId);

            foreach (var refundItem in eventData.Refund.RefundItems)
            {
                var order = await _orderRepository.GetAsync(refundItem.OrderId);

                foreach (var eto in refundItem.RefundItemOrderLines)
                {
                    order.Refund(eto.OrderLineId, eto.RefundedQuantity, eto.RefundAmount);
                }

                await _orderRepository.UpdateAsync(order, true);

                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(
                    new OrderRefundedEto(_objectMapper.Map<Order, OrderEto>(order), eventData.Refund))
                );

                await uow.CompleteAsync();
            }
        }
    }
}