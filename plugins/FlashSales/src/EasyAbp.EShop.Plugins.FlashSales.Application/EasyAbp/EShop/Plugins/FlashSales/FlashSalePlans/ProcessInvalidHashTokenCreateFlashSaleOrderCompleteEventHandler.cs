﻿using System;
using System.Threading.Tasks;
using EasyAbp.Eshop.Products.Products;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class ProcessInvalidHashTokenCreateFlashSaleOrderCompleteEventHandler : IDistributedEventHandler<CreateFlashSaleOrderCompleteEto>, ITransientDependency
{
    protected IFlashSaleInventoryManager FlashSaleInventoryManager { get; }
    protected IDistributedCache DistributedCache { get; }
    protected IFlashSalePlanRepository FlashSalePlanRepository { get; }
    protected IProductAppService ProductAppService { get; }
    protected ILogger<ProcessInvalidHashTokenCreateFlashSaleOrderCompleteEventHandler> Logger { get; }

    public ProcessInvalidHashTokenCreateFlashSaleOrderCompleteEventHandler(
        IFlashSaleInventoryManager flashSaleInventoryManager,
        IDistributedCache distributedCache,
        IFlashSalePlanRepository flashSalePlanRepository,
        IProductAppService productAppService,
        ILogger<ProcessInvalidHashTokenCreateFlashSaleOrderCompleteEventHandler> logger)
    {
        FlashSaleInventoryManager = flashSaleInventoryManager;
        DistributedCache = distributedCache;
        FlashSalePlanRepository = flashSalePlanRepository;
        ProductAppService = productAppService;
        Logger = logger;
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
        return Task.FromResult(string.Format(FlashSalePlanAppService.UserFlashSaleResultCacheKeyFormat, plan.TenantId, plan.Id, userId));
    }

    protected virtual async Task RemoveUserFlashSaleResultCacheAsync(FlashSalePlan plan, Guid userId)
    {
        await DistributedCache.RemoveAsync(await GetUserFlashSaleResultCacheKeyAsync(plan, userId));
    }
}