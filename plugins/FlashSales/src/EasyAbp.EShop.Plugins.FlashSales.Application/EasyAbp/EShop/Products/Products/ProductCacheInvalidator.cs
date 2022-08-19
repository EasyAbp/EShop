using System;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.Products;

public class ProductCacheInvalidator :
    IDistributedEventHandler<EntityUpdatedEto<ProductEto>>,
    IDistributedEventHandler<EntityDeletedEto<ProductEto>>,
    ITransientDependency
{
    protected IDistributedCache<ProductCacheItem, Guid> DistributedCache { get; }
    protected IUnitOfWorkManager UnitOfWorkManager { get; }

    public ProductCacheInvalidator(
        IDistributedCache<ProductCacheItem, Guid> distributedCache,
        IUnitOfWorkManager unitOfWorkManager)
    {
        DistributedCache = distributedCache;
        UnitOfWorkManager = unitOfWorkManager;
    }

    public virtual async Task HandleEventAsync(EntityUpdatedEto<ProductEto> eventData)
    {
        await DistributedCache.RemoveAsync(eventData.Entity.Id);

        UnitOfWorkManager.Current?.OnCompleted(async () =>
        {
            await DistributedCache.RemoveAsync(eventData.Entity.Id);
        });
    }

    public virtual async Task HandleEventAsync(EntityDeletedEto<ProductEto> eventData)
    {
        await DistributedCache.RemoveAsync(eventData.Entity.Id);

        UnitOfWorkManager.Current?.OnCompleted(async () =>
        {
            await DistributedCache.RemoveAsync(eventData.Entity.Id);
        });
    }
}
