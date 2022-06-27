using System;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public class FlashSalesPlanCacheInvalidator : ILocalEventHandler<EntityChangedEventData<FlashSalesPlan>>, ITransientDependency
{
    protected IDistributedCache<FlashSalesPlanCacheItem, Guid> DistributedCache { get; }

    public FlashSalesPlanCacheInvalidator(IDistributedCache<FlashSalesPlanCacheItem, Guid> distributedCache)
    {
        DistributedCache = distributedCache;
    }

    public virtual async Task HandleEventAsync(EntityChangedEventData<FlashSalesPlan> eventData)
    {
        await DistributedCache.RemoveAsync(eventData.Entity.Id);
    }
}
