using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderCreatedEventHandler : IDistributedEventHandler<EntityCreatedEto<OrderEto>>, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IDistributedEventBus _distributedEventBus;

        public OrderCreatedEventHandler(
            ICurrentTenant currentTenant,
            IDistributedEventBus distributedEventBus)
        {
            _currentTenant = currentTenant;
            _distributedEventBus = distributedEventBus;
        }
        
        public virtual async Task HandleEventAsync(EntityCreatedEto<OrderEto> eventData)
        {
            using var changeTenant = _currentTenant.Change(eventData.Entity.TenantId);

            await _distributedEventBus.PublishAsync(new ProductInventoryReductionAfterOrderPlacedResultEto
            {
                TenantId = eventData.Entity.TenantId,
                OrderId = eventData.Entity.Id,
                IsSuccess = true
            });
        }
    }
}