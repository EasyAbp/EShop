using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
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
    protected IServiceScopeFactory ServiceScopeFactory { get; }

    public ProductCacheInvalidator(
        IProductCache productCache,
        IUnitOfWorkManager unitOfWorkManager,
        IServiceScopeFactory serviceScopeFactory)
    {
        ProductCache = productCache;
        UnitOfWorkManager = unitOfWorkManager;
        ServiceScopeFactory = serviceScopeFactory;
    }

    public virtual async Task HandleEventAsync(EntityUpdatedEto<ProductEto> eventData)
    {
        await ProductCache.RemoveAsync(eventData.Entity.Id);

        UnitOfWorkManager.Current?.OnCompleted(async () =>
        {
            using var scope = ServiceScopeFactory.CreateScope();
            var productCache = scope.ServiceProvider.GetRequiredService<IProductCache>();
            await productCache.RemoveAsync(eventData.Entity.Id);
        });
    }

    public virtual async Task HandleEventAsync(EntityDeletedEto<ProductEto> eventData)
    {
        await ProductCache.RemoveAsync(eventData.Entity.Id);

        UnitOfWorkManager.Current?.OnCompleted(async () =>
        {
            using var scope = ServiceScopeFactory.CreateScope();
            var productCache = scope.ServiceProvider.GetRequiredService<IProductCache>();
            await productCache.RemoveAsync(eventData.Entity.Id);
        });
    }
}