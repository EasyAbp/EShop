using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class FlashSaleOrderCreationResultEventHandler : IDistributedEventHandler<FlashSaleOrderCreationResultEto>,
    ITransientDependency
{
    protected ILogger<FlashSaleOrderCreationResultEventHandler> Logger { get; }
    protected IUnitOfWorkManager UnitOfWorkManager { get; }
    protected IServiceScopeFactory ServiceScopeFactory { get; }
    protected IFlashSaleResultRepository FlashSaleResultRepository { get; }

    public FlashSaleOrderCreationResultEventHandler(
        ILogger<FlashSaleOrderCreationResultEventHandler> logger,
        IUnitOfWorkManager unitOfWorkManager,
        IServiceScopeFactory serviceScopeFactory,
        IFlashSaleResultRepository flashSaleResultRepository)
    {
        Logger = logger;
        UnitOfWorkManager = unitOfWorkManager;
        ServiceScopeFactory = serviceScopeFactory;
        FlashSaleResultRepository = flashSaleResultRepository;
    }

    [UnitOfWork]
    public virtual async Task HandleEventAsync(FlashSaleOrderCreationResultEto eventData)
    {
        var flashSaleResult = await FlashSaleResultRepository.GetAsync(eventData.ResultId);

        if (eventData.Success)
        {
            flashSaleResult.MarkAsSuccessful(eventData.OrderId!.Value);

            await FlashSaleResultRepository.UpdateAsync(flashSaleResult, autoSave: true);
        }
        else
        {
            flashSaleResult.MarkAsFailed(eventData.Reason);

            await FlashSaleResultRepository.UpdateAsync(flashSaleResult, autoSave: true);
        }

        UnitOfWorkManager.Current.OnCompleted(async () =>
        {
            using var scope = ServiceScopeFactory.CreateScope();

            var flashSaleInventoryManager = scope.ServiceProvider.GetRequiredService<IFlashSaleInventoryManager>();
            var flashSaleCurrentResultCache = scope.ServiceProvider.GetRequiredService<IFlashSaleCurrentResultCache>();
            var objectMapper = scope.ServiceProvider.GetRequiredService<IObjectMapper>();

            if (eventData.Success)
            {
                await ResetFlashSaleCurrentResultCacheAsync(flashSaleResult, objectMapper, flashSaleCurrentResultCache);
            }
            else
            {
                // try to roll back the inventory.
                if (!await flashSaleInventoryManager.TryRollBackInventoryAsync(
                        eventData.TenantId, eventData.ProductInventoryProviderName, eventData.StoreId,
                        eventData.ProductId, eventData.ProductSkuId))
                {
                    Logger.LogWarning("Failed to roll back the flash sale inventory.");
                    return; // avoid to remove cache if the rollback failed.
                }

                // remove the cache so the user can try to order again.
                if (eventData.AllowToTryAgain)
                {
                    await flashSaleCurrentResultCache.RemoveAsync(flashSaleResult.PlanId, flashSaleResult.UserId);
                }
                else
                {
                    await ResetFlashSaleCurrentResultCacheAsync(flashSaleResult, objectMapper, flashSaleCurrentResultCache);
                }
            }
        });
    }

    protected virtual async Task ResetFlashSaleCurrentResultCacheAsync(FlashSaleResult flashSaleResult,
        IObjectMapper objectMapper, IFlashSaleCurrentResultCache flashSaleCurrentResultCache)
    {
        await flashSaleCurrentResultCache.SetAsync(flashSaleResult.PlanId, flashSaleResult.UserId,
            new FlashSaleCurrentResultCacheItem
            {
                TenantId = flashSaleResult.TenantId,
                ResultDto = objectMapper.Map<FlashSaleResult, FlashSaleResultDto>(flashSaleResult)
            });
    }
}