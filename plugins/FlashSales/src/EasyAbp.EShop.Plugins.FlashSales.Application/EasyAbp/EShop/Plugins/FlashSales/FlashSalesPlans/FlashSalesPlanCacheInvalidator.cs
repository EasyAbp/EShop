using System;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public class FlashSalesPlanCacheInvalidator : ILocalEventHandler<EntityChangedEventData<FlashSalesPlan>>, ITransientDependency
{
    protected IDistributedCache<FlashSalesPlanCacheItem, Guid> DistributedCache { get; }
    protected IUnitOfWorkManager UnitOfWorkManager { get; }

    public FlashSalesPlanCacheInvalidator(
        IDistributedCache<FlashSalesPlanCacheItem, Guid> distributedCache,
        IUnitOfWorkManager unitOfWorkManager)
    {
        DistributedCache = distributedCache;
        UnitOfWorkManager = unitOfWorkManager;
    }

    public virtual Task HandleEventAsync(EntityChangedEventData<FlashSalesPlan> eventData)
    {
        UnitOfWorkManager.Current.OnCompleted(async () =>
        {
            await DistributedCache.RemoveAsync(eventData.Entity.Id);
        });

        return Task.CompletedTask;
    }
}
