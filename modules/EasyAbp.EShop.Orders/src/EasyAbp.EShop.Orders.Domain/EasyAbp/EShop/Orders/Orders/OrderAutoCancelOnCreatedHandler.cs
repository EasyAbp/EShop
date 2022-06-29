using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderAutoCancelOnCreatedHandler :
        IDistributedEventHandler<EntityCreatedEto<OrderEto>>,
        ITransientDependency
    {
        private readonly IClock _clock;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public OrderAutoCancelOnCreatedHandler(
            IClock clock,
            IBackgroundJobManager backgroundJobManager)
        {
            _clock = clock;
            _backgroundJobManager = backgroundJobManager;
        }

        public virtual async Task HandleEventAsync(EntityCreatedEto<OrderEto> eventData)
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

            if(_backgroundJobManager.IsAvailable())
            {
                await _backgroundJobManager.EnqueueAsync(
                    args: args,
                    delay: eventData.Entity.PaymentExpiration.Value.Subtract(_clock.Now) // Todo: use a absolute time.
                );
            }
        }
    }
}