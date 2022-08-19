using System.Threading.Tasks;
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
    protected IProductCache ProductCache { get; }
    protected IUnitOfWorkManager UnitOfWorkManager { get; }

    public ProductCacheInvalidator(
        IProductCache productCache,
        IUnitOfWorkManager unitOfWorkManager)
    {
        ProductCache = productCache;
        UnitOfWorkManager = unitOfWorkManager;
    }

    public virtual async Task HandleEventAsync(EntityUpdatedEto<ProductEto> eventData)
    {
        await ProductCache.RemoveAsync(eventData.Entity.Id);

        UnitOfWorkManager.Current?.OnCompleted(async () =>
        {
            await ProductCache.RemoveAsync(eventData.Entity.Id);
        });
    }

    public virtual async Task HandleEventAsync(EntityDeletedEto<ProductEto> eventData)
    {
        await ProductCache.RemoveAsync(eventData.Entity.Id);

        UnitOfWorkManager.Current?.OnCompleted(async () =>
        {
            await ProductCache.RemoveAsync(eventData.Entity.Id);
        });
    }
}
