using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    [Dependency(TryRegister = true)]
    [UnitOfWork]
    public class DefaultUnpaidOrderAutoCancelProvider :
        IUnpaidOrderAutoCancelProvider,
        ILocalEventHandler<EntityCreatedEventData<Order>>,
        ILocalEventHandler<OrderPaymentIdChangedEto>,
        ITransientDependency
    {
        private readonly IClock _clock;
        private readonly IOrderManager _orderManager;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public DefaultUnpaidOrderAutoCancelProvider(
            IClock clock,
            IOrderManager orderManager,
            IBackgroundJobManager backgroundJobManager)
        {
            _clock = clock;
            _orderManager = orderManager;
            _backgroundJobManager = backgroundJobManager;
        }
        
        public virtual async Task HandleEventAsync(EntityCreatedEventData<Order> eventData)
        {
            if (!eventData.Entity.PaymentExpiration.HasValue)
            {
                return;
            }

            var args = new UnpaidOrderAutoCancelArgs
            {
                TenantId = eventData.Entity.TenantId,
                OrderId = eventData.Entity.Id
            };

            await _backgroundJobManager.EnqueueAsync(
                args: args,
                delay: eventData.Entity.PaymentExpiration.Value.Subtract(_clock.Now)    // Todo: use a absolute time.
            );
        }

        public virtual async Task HandleEventAsync(OrderPaymentIdChangedEto eventData)
        {
            if (!eventData.Order.PaymentExpiration.HasValue || eventData.ToPaymentId.HasValue)
            {
                return;
            }

            if (_clock.Now > eventData.Order.PaymentExpiration.Value && !eventData.Order.CanceledTime.HasValue)
            {
                await _orderManager.CancelAsync(eventData.Order, OrdersConsts.CancellationReason);
            }
        }
    }
}