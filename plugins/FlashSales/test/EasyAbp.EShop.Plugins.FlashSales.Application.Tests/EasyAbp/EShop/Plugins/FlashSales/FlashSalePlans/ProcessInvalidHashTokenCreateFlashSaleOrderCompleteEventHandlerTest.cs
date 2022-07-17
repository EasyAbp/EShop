using System;
using System.Threading.Tasks;
using EasyAbp.Eshop.Products.Products;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class ProcessInvalidHashTokenCreateFlashSaleOrderCompleteEventHandlerTest : FlashSalesApplicationTestBase
{
    protected ProcessInvalidHashTokenCreateFlashSaleOrderCompleteEventHandler EventHandler { get; }

    protected IDistributedCache DistributedCache { get; }

    protected IFlashSaleInventoryManager FlashSaleInventoryManager { get; set; }

    private ProductDto Product1 { get; set; }

    public ProcessInvalidHashTokenCreateFlashSaleOrderCompleteEventHandlerTest()
    {
        EventHandler = GetRequiredService<ProcessInvalidHashTokenCreateFlashSaleOrderCompleteEventHandler>();
        DistributedCache = GetRequiredService<IDistributedCache>();
        FlashSaleInventoryManager = GetRequiredService<IFlashSaleInventoryManager>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        Product1 = CreateMockProductDto();

        var productAppService = Substitute.For<IProductAppService>();
        productAppService.GetAsync(FlashSalesTestData.Product1Id).Returns(Task.FromResult(Product1));
        services.Replace(ServiceDescriptor.Singleton(productAppService));

        FlashSaleInventoryManager = Substitute.For<IFlashSaleInventoryManager>();
        services.Replace(ServiceDescriptor.Singleton(FlashSaleInventoryManager));
        base.AfterAddApplication(services);
    }

    [Fact]
    public async Task HandleEventAsync()
    {
        var plan = await CreateFlashSalePlanAsync();
        var pendingFlashResult = await CreatePendingResultAsync(plan.Id, plan.StoreId, CurrentUser.GetId());
        var userFlashSaleResultCacheKey = string.Format(FlashSalePlanAppService.UserFlashSaleResultCacheKeyFormat, plan.TenantId, plan.Id, CurrentUser.GetId());
        await DistributedCache.SetStringAsync(userFlashSaleResultCacheKey, pendingFlashResult.Id.ToString());
        var createFlashSaleOrderCompleteEto = new CreateFlashSaleOrderCompleteEto()
        {
            TenantId = pendingFlashResult.TenantId,
            PendingResultId = pendingFlashResult.Id,
            Success = false,
            StoreId = pendingFlashResult.StoreId,
            PlanId = pendingFlashResult.PlanId,
            OrderId = null,
            Reason = FlashSaleResultFailedReason.InvalidHashToken,
            UserId = pendingFlashResult.UserId
        };
        FlashSaleInventoryManager
            .TryRollBackInventoryAsync(plan.TenantId, Product1.InventoryProviderName, plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true)
            .Returns(Task.FromResult(true));

        await EventHandler.HandleEventAsync(createFlashSaleOrderCompleteEto);

        var userFlashSaleResultCache = await DistributedCache.GetStringAsync(userFlashSaleResultCacheKey);
        userFlashSaleResultCache.ShouldBeNull();

        await FlashSaleInventoryManager.Received()
            .TryRollBackInventoryAsync(plan.TenantId, Product1.InventoryProviderName, plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true);
    }

    [Fact]
    public async Task HandleEventAsync_Should_Not_Remove_UserFlashSaleResultCache_When_TryRollBackInventory_Failed()
    {
        var plan = await CreateFlashSalePlanAsync();
        var pendingFlashResult = await CreatePendingResultAsync(plan.Id, plan.StoreId, CurrentUser.GetId());
        var userFlashSaleResultCacheKey = string.Format(FlashSalePlanAppService.UserFlashSaleResultCacheKeyFormat, plan.TenantId, plan.Id, CurrentUser.GetId());
        await DistributedCache.SetStringAsync(userFlashSaleResultCacheKey, pendingFlashResult.Id.ToString());
        var createFlashSaleOrderCompleteEto = new CreateFlashSaleOrderCompleteEto()
        {
            TenantId = pendingFlashResult.TenantId,
            PendingResultId = pendingFlashResult.Id,
            Success = false,
            StoreId = pendingFlashResult.StoreId,
            PlanId = pendingFlashResult.PlanId,
            OrderId = null,
            Reason = FlashSaleResultFailedReason.InvalidHashToken,
            UserId = pendingFlashResult.UserId
        };
        FlashSaleInventoryManager
            .TryRollBackInventoryAsync(plan.TenantId, Product1.InventoryProviderName, plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true)
            .Returns(Task.FromResult(false));

        await EventHandler.HandleEventAsync(createFlashSaleOrderCompleteEto);

        var userFlashSaleResultCache = await DistributedCache.GetStringAsync(userFlashSaleResultCacheKey);
        userFlashSaleResultCache.ShouldBe(pendingFlashResult.Id.ToString());

        await FlashSaleInventoryManager.Received()
            .TryRollBackInventoryAsync(plan.TenantId, Product1.InventoryProviderName, plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true);
    }

    [Fact]
    public async Task HandleEventAsync_Should_Ignore_When_Success_Is_True()
    {
        var plan = await CreateFlashSalePlanAsync();
        var pendingFlashResult = await CreatePendingResultAsync(plan.Id, plan.StoreId, CurrentUser.GetId());
        var userFlashSaleResultCacheKey = string.Format(FlashSalePlanAppService.UserFlashSaleResultCacheKeyFormat, plan.TenantId, plan.Id, CurrentUser.GetId());
        await DistributedCache.SetStringAsync(userFlashSaleResultCacheKey, pendingFlashResult.Id.ToString());
        var createFlashSaleOrderCompleteEto = new CreateFlashSaleOrderCompleteEto()
        {
            TenantId = pendingFlashResult.TenantId,
            PendingResultId = pendingFlashResult.Id,
            Success = true,
            StoreId = pendingFlashResult.StoreId,
            PlanId = pendingFlashResult.PlanId,
            OrderId = Guid.NewGuid(),
            Reason = null,
            UserId = pendingFlashResult.UserId
        };

        await EventHandler.HandleEventAsync(createFlashSaleOrderCompleteEto);

        var userFlashSaleResultCache = await DistributedCache.GetStringAsync(userFlashSaleResultCacheKey);
        userFlashSaleResultCache.ShouldBe(pendingFlashResult.Id.ToString());

        await FlashSaleInventoryManager.DidNotReceiveWithAnyArgs()
            .TryRollBackInventoryAsync(default, default, Guid.Empty, Guid.Empty, Guid.Empty, default, default);
    }

    [Fact]
    public async Task HandleEventAsync_Should_Ignore_When_Reason_Not_InvalidHashToken()
    {
        var plan = await CreateFlashSalePlanAsync();
        var pendingFlashResult = await CreatePendingResultAsync(plan.Id, plan.StoreId, CurrentUser.GetId());
        var userFlashSaleResultCacheKey = string.Format(FlashSalePlanAppService.UserFlashSaleResultCacheKeyFormat, plan.TenantId, plan.Id, CurrentUser.GetId());
        await DistributedCache.SetStringAsync(userFlashSaleResultCacheKey, pendingFlashResult.Id.ToString());
        var createFlashSaleOrderCompleteEto = new CreateFlashSaleOrderCompleteEto()
        {
            TenantId = pendingFlashResult.TenantId,
            PendingResultId = pendingFlashResult.Id,
            Success = false,
            StoreId = pendingFlashResult.StoreId,
            PlanId = pendingFlashResult.PlanId,
            OrderId = null,
            Reason = "Other",
            UserId = pendingFlashResult.UserId
        };

        await EventHandler.HandleEventAsync(createFlashSaleOrderCompleteEto);

        var userFlashSaleResultCache = await DistributedCache.GetStringAsync(userFlashSaleResultCacheKey);
        userFlashSaleResultCache.ShouldBe(pendingFlashResult.Id.ToString());

        await FlashSaleInventoryManager.DidNotReceiveWithAnyArgs()
            .TryRollBackInventoryAsync(default, default, Guid.Empty, Guid.Empty, Guid.Empty, default, default);
    }
}
