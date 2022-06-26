using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public class FlashSalesPlanCacheInvalidator : ILocalEventHandler<EntityChangedEventData<FlashSalesPlan>>
{
    protected IDistributedCache<FlashSalesPlanCacheItem, Guid> DistributedCache { get; }

    public virtual async Task HandleEventAsync(EntityChangedEventData<FlashSalesPlan> eventData)
    {
        await DistributedCache.RemoveAsync(eventData.Entity.Id);
    }
}
