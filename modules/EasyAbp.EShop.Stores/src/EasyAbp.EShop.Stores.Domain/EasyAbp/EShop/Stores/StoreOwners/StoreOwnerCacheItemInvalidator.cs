using System;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    public class StoreOwnerCacheItemInvalidator : ILocalEventHandler<EntityChangedEventData<StoreOwner>>, ITransientDependency
    {
        protected ICurrentTenant CurrentTenant { get; }

        protected IDistributedCache<StoreOwnerCacheItem> Cache { get; }

        public StoreOwnerCacheItemInvalidator(IDistributedCache<StoreOwnerCacheItem> cache, ICurrentTenant currentTenant)
        {
            Cache = cache;
            CurrentTenant = currentTenant;
        }

        public virtual async Task HandleEventAsync(EntityChangedEventData<StoreOwner> eventData)
        {
            var cacheKey = CalculateCacheKey(
                eventData.Entity.StoreId,
                eventData.Entity.OwnerUserId
            );

            using (CurrentTenant.Change(eventData.Entity.TenantId))
            {
                await Cache.RemoveAsync(cacheKey);
            }
        }

        protected virtual string CalculateCacheKey(Guid storeId, Guid userId)
        {
            return StoreOwnerCacheItem.CalculateCacheKey(storeId, userId);
        }
    }
}