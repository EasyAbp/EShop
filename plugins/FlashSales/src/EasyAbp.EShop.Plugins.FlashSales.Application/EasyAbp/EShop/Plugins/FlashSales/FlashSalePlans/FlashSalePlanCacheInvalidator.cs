using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class FlashSalePlanCacheInvalidator : ILocalEventHandler<EntityChangedEventData<FlashSalePlan>>,
    ITransientDependency
{
    protected IServiceScopeFactory ServiceScopeFactory { get; }
    protected IDistributedCache<FlashSalePlanCacheItem, Guid> DistributedCache { get; }
    protected IUnitOfWorkManager UnitOfWorkManager { get; }

    public FlashSalePlanCacheInvalidator(
        IServiceScopeFactory serviceScopeFactory,
        IDistributedCache<FlashSalePlanCacheItem, Guid> distributedCache,
        IUnitOfWorkManager unitOfWorkManager)
    {
        ServiceScopeFactory = serviceScopeFactory;
        DistributedCache = distributedCache;
        UnitOfWorkManager = unitOfWorkManager;
    }

    public virtual async Task HandleEventAsync(EntityChangedEventData<FlashSalePlan> eventData)
    {
        await DistributedCache.RemoveAsync(eventData.Entity.Id);

        UnitOfWorkManager.Current?.OnCompleted(async () =>
        {
            using var scope = ServiceScopeFactory.CreateScope();

            var distributedCache =
                scope.ServiceProvider.GetRequiredService<IDistributedCache<FlashSalePlanCacheItem, Guid>>();

            await distributedCache.RemoveAsync(eventData.Entity.Id);
        });
    }
}