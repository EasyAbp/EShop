using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using EasyAbp.Eshop.Products.Products;
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
    protected IFlashSaleInventoryManager FlashSaleInventoryManager { get; }
    protected IUnitOfWorkManager UnitOfWorkManager { get; }
    protected IServiceScopeFactory ServiceScopeFactory { get; }
    protected IFlashSaleCurrentResultCache FlashSaleCurrentResultCache { get; }
    protected IFlashSaleResultRepository FlashSaleResultRepository { get; }

    public FlashSaleOrderCreationResultEventHandler(
        ILogger<FlashSaleOrderCreationResultEventHandler> logger,
        IFlashSaleInventoryManager flashSaleInventoryManager,
        IUnitOfWorkManager unitOfWorkManager,
        IServiceScopeFactory serviceScopeFactory,
        IFlashSaleCurrentResultCache flashSaleCurrentResultCache,
        IFlashSaleResultRepository flashSaleResultRepository)
    {
        Logger = logger;
        FlashSaleInventoryManager = flashSaleInventoryManager;
        UnitOfWorkManager = unitOfWorkManager;
        ServiceScopeFactory = serviceScopeFactory;
        FlashSaleCurrentResultCache = flashSaleCurrentResultCache;
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
            if (eventData.Success)
            {
                await ResetFlashSaleCurrentResultCacheAsync(flashSaleResult);
            }
            else
            {
                // try to roll back the inventory.
                if (!await FlashSaleInventoryManager.TryRollBackInventoryAsync(
                        eventData.TenantId, eventData.ProductInventoryProviderName, eventData.StoreId,
                        eventData.ProductId, eventData.ProductSkuId))
                {
                    Logger.LogWarning("Failed to roll back the flash sale inventory.");
                    return; // avoid to remove cache if the rollback failed.
                }

                // remove the cache so the user can try to order again.
                if (eventData.AllowToTryAgain)
                {
                    await FlashSaleCurrentResultCache.RemoveAsync(flashSaleResult.PlanId, flashSaleResult.UserId);
                }
                else
                {
                    await ResetFlashSaleCurrentResultCacheAsync(flashSaleResult);
                }
            }
        });
    }

    protected virtual async Task ResetFlashSaleCurrentResultCacheAsync(FlashSaleResult flashSaleResult)
    {
        using var scope = ServiceScopeFactory.CreateScope();

        var objectMapper = scope.ServiceProvider.GetRequiredService<IObjectMapper>();
        var flashSaleCurrentResultCache = scope.ServiceProvider.GetRequiredService<IFlashSaleCurrentResultCache>();

        await flashSaleCurrentResultCache.SetAsync(flashSaleResult.PlanId, flashSaleResult.UserId,
            new FlashSaleCurrentResultCacheItem
            {
                TenantId = flashSaleResult.TenantId,
                ResultDto = objectMapper.Map<FlashSaleResult, FlashSaleResultDto>(flashSaleResult)
            });
    }
}