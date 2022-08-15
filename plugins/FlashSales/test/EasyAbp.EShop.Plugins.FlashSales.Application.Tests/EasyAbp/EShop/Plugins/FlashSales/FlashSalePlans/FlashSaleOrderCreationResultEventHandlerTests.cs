using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.Eshop.Products.Products;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using Xunit;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class FlashSaleOrderCreationResultEventHandlerTests : FlashSalesApplicationTestBase
{
    protected IObjectMapper ObjectMapper { get; }
    protected IFlashSaleCurrentResultCache FlashSaleCurrentResultCache { get; }
    protected FlashSaleOrderCreationResultEventHandler FlashSaleOrderCreationResultEventHandler { get; }
    protected IFlashSaleInventoryManager FlashSaleInventoryManager { get; set; }

    private ProductDto Product1 { get; set; }

    public FlashSaleOrderCreationResultEventHandlerTests()
    {
        ObjectMapper = GetRequiredService<IObjectMapper>();
        FlashSaleCurrentResultCache = GetRequiredService<IFlashSaleCurrentResultCache>();
        FlashSaleOrderCreationResultEventHandler = GetRequiredService<FlashSaleOrderCreationResultEventHandler>();
        FlashSaleInventoryManager = GetRequiredService<IFlashSaleInventoryManager>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        Product1 = CreateMockProductDto();

        var productAppService = Substitute.For<IProductAppService>();
        productAppService.GetAsync(FlashSalesTestData.Product1Id).Returns(Task.FromResult(Product1));
        services.Replace(ServiceDescriptor.Singleton(productAppService));

        var productDetailAppService = Substitute.For<IProductDetailAppService>();
        services.Replace(ServiceDescriptor.Singleton(productDetailAppService));
        productDetailAppService.GetAsync(FlashSalesTestData.ProductDetail1Id).Returns(Task.FromResult(
            new ProductDetailDto
            {
                Id = FlashSalesTestData.ProductDetail1Id,
                CreationTime = FlashSalesTestData.ProductDetailLastModificationTime,
                LastModificationTime = FlashSalesTestData.ProductDetailLastModificationTime,
                StoreId = FlashSalesTestData.Store1Id,
                Description = "My Details 1"
            }));
        productDetailAppService.GetAsync(FlashSalesTestData.ProductDetail2Id).Returns(Task.FromResult(
            new ProductDetailDto
            {
                Id = FlashSalesTestData.ProductDetail2Id,
                StoreId = FlashSalesTestData.Store1Id,
                Description = "My Details 2"
            }));

        var flashSaleInventoryManager = Substitute.For<IFlashSaleInventoryManager>();
        services.Replace(ServiceDescriptor.Singleton(flashSaleInventoryManager));

        base.AfterAddApplication(services);
    }

    protected async Task SetFlashSaleCurrentResultCacheAsync(FlashSaleResult flashSaleResult)
    {
        await FlashSaleCurrentResultCache.SetAsync(flashSaleResult.PlanId, flashSaleResult.UserId,
            new FlashSaleCurrentResultCacheItem
            {
                TenantId = CurrentTenant.Id,
                ResultDto = ObjectMapper.Map<FlashSaleResult, FlashSaleResultDto>(flashSaleResult)
            });
    }

    [Fact]
    public async Task HandleEventAsync_When_Create_Order_Success()
    {
        var plan = await CreateFlashSalePlanAsync();
        var flashSaleResult = await CreatePendingResultAsync(plan.Id, plan.StoreId, CurrentUser.GetId());
        await SetFlashSaleCurrentResultCacheAsync(flashSaleResult);

        var flashSaleOrderCreationResultEto = new FlashSaleOrderCreationResultEto
        {
            TenantId = flashSaleResult.TenantId,
            ResultId = flashSaleResult.Id,
            Success = true,
            StoreId = flashSaleResult.StoreId,
            PlanId = flashSaleResult.PlanId,
            OrderId = GuidGenerator.Create(),
            Reason = null,
            UserId = flashSaleResult.UserId,
            AllowToTryAgain = false
        };

        FlashSaleInventoryManager
            .TryRollBackInventoryAsync(plan.TenantId, Product1.InventoryProviderName, plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true)
            .Returns(Task.FromResult(true));

        await FlashSaleOrderCreationResultEventHandler.HandleEventAsync(flashSaleOrderCreationResultEto);

        var flashResult = await FlashSaleResultRepository.GetAsync(flashSaleResult.Id);
        flashResult.Status.ShouldBe(FlashSaleResultStatus.Successful);
        flashResult.OrderId.ShouldBe(flashSaleOrderCreationResultEto.OrderId);
        flashResult.Reason.ShouldBe(null);

        var flashSaleCurrentResultCache = await FlashSaleCurrentResultCache.GetAsync(flashResult.PlanId, flashResult.UserId);
        flashSaleCurrentResultCache.ShouldNotBeNull();
        flashSaleCurrentResultCache.ResultDto.Id.ShouldBe(flashSaleResult.Id);
        flashSaleCurrentResultCache.ResultDto.Status.ShouldBe(FlashSaleResultStatus.Successful);

        await FlashSaleInventoryManager.DidNotReceive()
            .TryRollBackInventoryAsync(plan.TenantId, Product1.InventoryProviderName, plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true);
    }

    [Fact]
    public async Task HandleEventAsync_When_Create_Order_Failed_And_Not_Allow_To_Try_Again()
    {
        var plan = await CreateFlashSalePlanAsync();
        var flashSaleResult = await CreatePendingResultAsync(plan.Id, plan.StoreId, CurrentUser.GetId());
        await SetFlashSaleCurrentResultCacheAsync(flashSaleResult);

        var flashSaleOrderCreationResultEto = new FlashSaleOrderCreationResultEto
        {
            TenantId = flashSaleResult.TenantId,
            ResultId = flashSaleResult.Id,
            Success = false,
            StoreId = FlashSalesTestData.Store1Id,
            PlanId = flashSaleResult.PlanId,
            OrderId = null,
            Reason = "Failed reason",
            UserId = flashSaleResult.UserId,
            AllowToTryAgain = false
        };

        FlashSaleInventoryManager
            .TryRollBackInventoryAsync(plan.TenantId, Product1.InventoryProviderName, plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true)
            .Returns(Task.FromResult(true));

        await FlashSaleOrderCreationResultEventHandler.HandleEventAsync(flashSaleOrderCreationResultEto);

        var flashResult = await FlashSaleResultRepository.GetAsync(flashSaleResult.Id);
        flashResult.Status.ShouldBe(FlashSaleResultStatus.Failed);
        flashResult.OrderId.ShouldBe(null);
        flashResult.Reason.ShouldBe("Failed reason");

        var flashSaleCurrentResultCache = await FlashSaleCurrentResultCache.GetAsync(flashResult.PlanId, flashResult.UserId);
        flashSaleCurrentResultCache.ShouldNotBeNull();
        flashSaleCurrentResultCache.ResultDto.Id.ShouldBe(flashSaleResult.Id);
        flashSaleCurrentResultCache.ResultDto.Status.ShouldBe(FlashSaleResultStatus.Failed);

        await FlashSaleInventoryManager.Received()
            .TryRollBackInventoryAsync(plan.TenantId, Product1.InventoryProviderName, plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true);
    }

    [Fact]
    public async Task HandleEventAsync_When_Create_Order_Failed_And_Allow_To_Try_Again()
    {
        var plan = await CreateFlashSalePlanAsync();
        var flashSaleResult = await CreatePendingResultAsync(plan.Id, plan.StoreId, CurrentUser.GetId());
        await SetFlashSaleCurrentResultCacheAsync(flashSaleResult);

        var flashSaleOrderCreationResultEto = new FlashSaleOrderCreationResultEto
        {
            TenantId = flashSaleResult.TenantId,
            ResultId = flashSaleResult.Id,
            Success = false,
            StoreId = FlashSalesTestData.Store1Id,
            PlanId = flashSaleResult.PlanId,
            OrderId = null,
            Reason = "Failed reason",
            UserId = flashSaleResult.UserId,
            AllowToTryAgain = true
        };

        FlashSaleInventoryManager
            .TryRollBackInventoryAsync(plan.TenantId, Product1.InventoryProviderName, plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true)
            .Returns(Task.FromResult(true));

        await FlashSaleOrderCreationResultEventHandler.HandleEventAsync(flashSaleOrderCreationResultEto);

        var flashResult = await FlashSaleResultRepository.GetAsync(flashSaleResult.Id);
        flashResult.Status.ShouldBe(FlashSaleResultStatus.Failed);
        flashResult.OrderId.ShouldBe(null);
        flashResult.Reason.ShouldBe("Failed reason");

        var flashSaleCurrentResultCache = await FlashSaleCurrentResultCache.GetAsync(flashResult.PlanId, flashResult.UserId);
        flashSaleCurrentResultCache.ShouldBeNull();

        await FlashSaleInventoryManager.Received()
            .TryRollBackInventoryAsync(plan.TenantId, Product1.InventoryProviderName, plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true);
    }

    [Fact]
    public async Task Should_Not_Remove_FlashSaleCurrentResultCache_When_TryRollBackInventory_Failed()
    {
        var plan = await CreateFlashSalePlanAsync();
        var flashSaleResult = await CreatePendingResultAsync(plan.Id, plan.StoreId, CurrentUser.GetId());
        await SetFlashSaleCurrentResultCacheAsync(flashSaleResult);

        var flashSaleOrderCreationResultEto = new FlashSaleOrderCreationResultEto
        {
            TenantId = flashSaleResult.TenantId,
            ResultId = flashSaleResult.Id,
            Success = false,
            StoreId = flashSaleResult.StoreId,
            PlanId = flashSaleResult.PlanId,
            OrderId = null,
            Reason = FlashSaleResultFailedReason.InvalidHashToken,
            UserId = flashSaleResult.UserId,
            AllowToTryAgain = true
        };

        FlashSaleInventoryManager
            .TryRollBackInventoryAsync(plan.TenantId, Product1.InventoryProviderName, plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true)
            .Returns(Task.FromResult(false));

        await FlashSaleOrderCreationResultEventHandler.HandleEventAsync(flashSaleOrderCreationResultEto);

        var flashSaleCurrentResultCache = await FlashSaleCurrentResultCache.GetAsync(plan.Id, CurrentUser.GetId());
        flashSaleCurrentResultCache.ShouldNotBeNull();

        await FlashSaleInventoryManager.Received()
            .TryRollBackInventoryAsync(plan.TenantId, Product1.InventoryProviderName, plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true);
    }
}
