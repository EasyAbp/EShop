using System;
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
        public static Guid LastOrderId { get; set; }
        public static bool SkippedHandling { get; set; }

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
            LastOrderId = eventData.Entity.Id;

            // skip if an order contains no OrderLine with `InventoryStrategy.ReduceAfterPlacing`.
            // see https://github.com/EasyAbp/EShop/issues/214
            if (eventData.Entity.ReducedInventoryAfterPlacingTime is not null)
            {
                SkippedHandling = true;
                return;
            }

            SkippedHandling = false;

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