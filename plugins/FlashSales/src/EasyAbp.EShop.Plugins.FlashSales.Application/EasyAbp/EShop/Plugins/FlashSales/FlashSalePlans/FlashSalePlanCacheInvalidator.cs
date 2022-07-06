using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public class FlashSalePlanCacheInvalidator : ILocalEventHandler<EntityChangedEventData<FlashSalePlan>>, ITransientDependency
{
    protected IDistributedCache<FlashSalePlanCacheItem, Guid> DistributedCache { get; }
    protected IUnitOfWorkManager UnitOfWorkManager { get; }

    public FlashSalePlanCacheInvalidator(
        IDistributedCache<FlashSalePlanCacheItem, Guid> distributedCache,
        IUnitOfWorkManager unitOfWorkManager)
    {
        DistributedCache = distributedCache;
        UnitOfWorkManager = unitOfWorkManager;
    }

    public virtual async Task HandleEventAsync(EntityChangedEventData<FlashSalePlan> eventData)
    {
        await DistributedCache.RemoveAsync(eventData.Entity.Id);

        UnitOfWorkManager.Current.OnCompleted(async () =>
        {
            await DistributedCache.RemoveAsync(eventData.Entity.Id);
        });
    }
}
