using System;
using System.Threading.Tasks;
using EasyAbp.Eshop.Products.Products;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class RollBackInventoryCreateFlashSaleOrderCompleteEventHandler : IDistributedEventHandler<CreateFlashSaleOrderCompleteEto>, ITransientDependency
{
    protected IFlashSaleInventoryManager FlashSaleInventoryManager { get; }
    protected IDistributedCache DistributedCache { get; }
    protected IFlashSalePlanRepository FlashSalePlanRepository { get; }
    protected IProductAppService ProductAppService { get; }
    protected ILogger<RollBackInventoryCreateFlashSaleOrderCompleteEventHandler> Logger { get; }

    public RollBackInventoryCreateFlashSaleOrderCompleteEventHandler(
        IFlashSaleInventoryManager flashSaleInventoryManager,
        IDistributedCache distributedCache,
        IFlashSalePlanRepository flashSalePlanRepository,
        IProductAppService productAppService)
    {
        FlashSaleInventoryManager = flashSaleInventoryManager;
        DistributedCache = distributedCache;
        FlashSalePlanRepository = flashSalePlanRepository;
        ProductAppService = productAppService;
    }

    public virtual async Task HandleEventAsync(CreateFlashSaleOrderCompleteEto eventData)
    {
        if (eventData.Success)
        {
            return;
        }

        if (eventData.Reason != FlashSaleResultFailedReason.InvalidHashToken)
        {
            return;
        }

        var plan = await FlashSalePlanRepository.GetAsync(eventData.PlanId);
        var product = await ProductAppService.GetAsync(plan.ProductId);

        if (!await FlashSaleInventoryManager.TryRollBackInventoryAsync(
            plan.TenantId, product.InventoryProviderName,
            plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true
        ))
        {
            Logger.LogWarning("Try roll back inventory failed.");
            return;
        }

        await RemoveUserFlashSaleResultCacheAsync(plan, eventData.UserId);
    }

    protected virtual Task<string> GetUserFlashSaleResultCacheKeyAsync(FlashSalePlan plan, Guid userId)
    {
        return Task.FromResult($"flash-sale-result-{plan.Id}-{userId}");
    }

    protected virtual async Task RemoveUserFlashSaleResultCacheAsync(FlashSalePlan plan, Guid userId)
    {
        await DistributedCache.RemoveAsync(await GetUserFlashSaleResultCacheKeyAsync(plan, userId));
    }
}
