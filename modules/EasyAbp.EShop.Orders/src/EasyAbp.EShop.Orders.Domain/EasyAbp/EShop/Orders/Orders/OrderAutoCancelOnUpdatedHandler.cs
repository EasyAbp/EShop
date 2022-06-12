using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderAutoCancelOnUpdatedHandler :
        ILocalEventHandler<EntityUpdatedEventData<Order>>,
        ITransientDependency
    {
        private readonly IClock _clock;
        private readonly IOrderManager _orderManager;

        public OrderAutoCancelOnUpdatedHandler(
            IClock clock,
            IOrderManager orderManager)
        {
            _clock = clock;
            _orderManager = orderManager;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityUpdatedEventData<Order> eventData)
        {
            if (!eventData.Entity.PaymentExpiration.HasValue || eventData.Entity.PaymentId.HasValue)
            {
                return;
            }

            if (_clock.Now > eventData.Entity.PaymentExpiration.Value && !eventData.Entity.CanceledTime.HasValue)
            {
                await _orderManager.CancelAsync(eventData.Entity, OrdersConsts.UnpaidAutoCancellationReason);
            }
        }
    }
}