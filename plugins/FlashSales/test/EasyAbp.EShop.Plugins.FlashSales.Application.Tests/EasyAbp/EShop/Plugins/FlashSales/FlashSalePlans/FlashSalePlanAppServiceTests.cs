using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.Options;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Users;
using Xunit;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
public class FlashSalePlanAppServiceTests : FlashSalesApplicationTestBase
{
    protected IFlashSalePlanAppService AppService { get; }

    protected IDistributedEventBus DistributedEventBus { get; }

    private ProductDto Product1 { get; set; }

    public FlashSalePlanAppServiceTests()
    {
        AppService = GetRequiredService<FlashSalePlanAppService>();
        DistributedEventBus = GetRequiredService<IDistributedEventBus>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        Product1 = CreateMockProductDto();

        var productAppService = Substitute.For<IProductAppService>();
        productAppService.GetAsync(FlashSalesTestData.Product1Id).Returns(Task.FromResult(Product1));
        services.Replace(ServiceDescriptor.Singleton(productAppService));

        services.Replace(ServiceDescriptor.Transient<IFlashSaleInventoryManager, FakeFlashSaleInventoryManager>());

        var distributedEventBus = Substitute.For<IDistributedEventBus>();
        services.Replace(ServiceDescriptor.Singleton(distributedEventBus));

        ObjectExtensionManager.Instance.AddOrUpdate<OrderFlashSalePlanInput>(info =>
        {
            info.AddOrUpdateProperty<string>("key1");
        });

        services.Replace(ServiceDescriptor.Transient<IProductCache, FakeProductCache>());

        base.AfterAddApplication(services);
    }

    [Fact]
    public async Task GetAsync()
    {
        var returnFlashSalePlan = await CreateFlashSalePlanAsync();

        var flashSalePlan = await AppService.GetAsync(returnFlashSalePlan.Id);

        flashSalePlan.ShouldNotBeNull();
        flashSalePlan.Id.ShouldBe(returnFlashSalePlan.Id);
        flashSalePlan.StoreId.ShouldBe(returnFlashSalePlan.StoreId);
        flashSalePlan.BeginTime.ShouldBe(returnFlashSalePlan.BeginTime);
        flashSalePlan.EndTime.ShouldBe(returnFlashSalePlan.EndTime);
        flashSalePlan.ProductId.ShouldBe(returnFlashSalePlan.ProductId);
        flashSalePlan.ProductSkuId.ShouldBe(returnFlashSalePlan.ProductSkuId);
        flashSalePlan.IsPublished.ShouldBe(returnFlashSalePlan.IsPublished);

        returnFlashSalePlan = await CreateFlashSalePlanAsync(isPublished: false);

        flashSalePlan = await AppService.GetAsync(returnFlashSalePlan.Id);

        flashSalePlan.ShouldNotBeNull();
        flashSalePlan.Id.ShouldBe(returnFlashSalePlan.Id);
        flashSalePlan.StoreId.ShouldBe(returnFlashSalePlan.StoreId);
        flashSalePlan.BeginTime.ShouldBe(returnFlashSalePlan.BeginTime);
        flashSalePlan.EndTime.ShouldBe(returnFlashSalePlan.EndTime);
        flashSalePlan.ProductId.ShouldBe(returnFlashSalePlan.ProductId);
        flashSalePlan.ProductSkuId.ShouldBe(returnFlashSalePlan.ProductSkuId);
        flashSalePlan.IsPublished.ShouldBe(returnFlashSalePlan.IsPublished);
    }

    [Fact]
    public async Task GetListAsync()
    {
        var publishedPlan = await CreateFlashSalePlanAsync(isPublished: true);
        var unpublishedPlan = await CreateFlashSalePlanAsync(isPublished: false);

        var allListDto = await AppService.GetListAsync(new FlashSalePlanGetListInput()
        {
            IncludeUnpublished = true
        });

        allListDto.TotalCount.ShouldBeGreaterThan(0);
        allListDto.Items.FirstOrDefault(x => x.Id == publishedPlan.Id)
            .ShouldNotBeNull()
            .Id.ShouldBe(publishedPlan.Id);

        allListDto.Items.FirstOrDefault(x => x.Id == unpublishedPlan.Id)
            .ShouldNotBeNull()
            .Id.ShouldBe(unpublishedPlan.Id);

        var publishedListDto = await AppService.GetListAsync(new FlashSalePlanGetListInput());

        publishedListDto.TotalCount.ShouldBeGreaterThan(0);
        publishedListDto.Items.FirstOrDefault(x => x.Id == publishedPlan.Id)
            .ShouldNotBeNull()
            .Id.ShouldBe(publishedPlan.Id);

        publishedListDto.Items.FirstOrDefault(x => x.Id == unpublishedPlan.Id)
            .ShouldBeNull();
    }

    [Fact]
    public async Task CreateAsync()
    {
        var createDto = new FlashSalePlanCreateDto()
        {
            StoreId = FlashSalesTestData.Store1Id,
            BeginTime = DateTime.Now,
            EndTime = DateTime.Now.AddMinutes(30),
            ProductId = FlashSalesTestData.Product1Id,
            ProductSkuId = FlashSalesTestData.ProductSku1Id,
            IsPublished = true
        };
        var returnFlashSalePlanDto = await AppService.CreateAsync(createDto);

        var flashSalePlan = await AppService.GetAsync(returnFlashSalePlanDto.Id);

        flashSalePlan.Id.ShouldBe(returnFlashSalePlanDto.Id);
        flashSalePlan.StoreId.ShouldBe(createDto.StoreId);
        flashSalePlan.BeginTime.ShouldBe(createDto.BeginTime);
        flashSalePlan.EndTime.ShouldBe(createDto.EndTime);
        flashSalePlan.ProductId.ShouldBe(createDto.ProductId);
        flashSalePlan.ProductSkuId.ShouldBe(createDto.ProductSkuId);
        flashSalePlan.IsPublished.ShouldBe(createDto.IsPublished);
    }

    [Fact]
    public async Task CreateAsync_Should_Throw_Exception_When_Validate_Product_Failed()
    {
        var createDto = new FlashSalePlanCreateDto()
        {
            StoreId = FlashSalesTestData.Store1Id,
            BeginTime = DateTime.Now,
            EndTime = DateTime.Now.AddMinutes(30),
            ProductId = FlashSalesTestData.Product1Id,
            ProductSkuId = FlashSalesTestData.ProductSku1Id,
            IsPublished = true
        };

        Product1.StoreId = Guid.NewGuid();
        await AppService.CreateAsync(createDto).ShouldThrowAsync<ProductIsNotInThisStoreException>();
        Product1.StoreId = FlashSalesTestData.Store1Id;

        Product1.InventoryStrategy = InventoryStrategy.ReduceAfterPlacing;
        await AppService.CreateAsync(createDto).ShouldThrowAsync<UnexpectedInventoryStrategyException>();

        Product1.StoreId = FlashSalesTestData.Store1Id;
        Product1.InventoryStrategy = InventoryStrategy.FlashSales;
    }

    [Fact]
    public async Task UpdateAsync()
    {
        var createDto = new FlashSalePlanCreateDto()
        {
            StoreId = FlashSalesTestData.Store1Id,
            BeginTime = DateTime.Now,
            EndTime = DateTime.Now.AddMinutes(30),
            ProductId = FlashSalesTestData.Product1Id,
            ProductSkuId = FlashSalesTestData.ProductSku1Id,
            IsPublished = true
        };
        var returnFlashSalePlanDto = await AppService.CreateAsync(createDto);
        var updateDto = new FlashSalePlanUpdateDto()
        {
            BeginTime = DateTime.Now.AddMinutes(30),
            EndTime = DateTime.Now.AddMinutes(60),
            ProductId = FlashSalesTestData.Product1Id,
            ProductSkuId = FlashSalesTestData.ProductSku1Id,
            IsPublished = false
        };

        var flashSalePlan = await AppService.UpdateAsync(returnFlashSalePlanDto.Id, updateDto);

        flashSalePlan.Id.ShouldBe(returnFlashSalePlanDto.Id);
        flashSalePlan.StoreId.ShouldBe(createDto.StoreId);
        flashSalePlan.BeginTime.ShouldBe(updateDto.BeginTime);
        flashSalePlan.EndTime.ShouldBe(updateDto.EndTime);
        flashSalePlan.ProductId.ShouldBe(updateDto.ProductId);
        flashSalePlan.ProductSkuId.ShouldBe(updateDto.ProductSkuId);
        flashSalePlan.IsPublished.ShouldBe(updateDto.IsPublished);
    }

    [Fact]
    public async Task UpdateAsync_Should_Throw_Exception_When_Validate_Product_Failed()
    {
        var createDto = new FlashSalePlanCreateDto()
        {
            StoreId = FlashSalesTestData.Store1Id,
            BeginTime = DateTime.Now,
            EndTime = DateTime.Now.AddMinutes(30),
            ProductId = FlashSalesTestData.Product1Id,
            ProductSkuId = FlashSalesTestData.ProductSku1Id,
            IsPublished = true
        };
        var returnFlashSalePlanDto = await AppService.CreateAsync(createDto);
        var updateDto = new FlashSalePlanUpdateDto()
        {
            BeginTime = DateTime.Now.AddMinutes(30),
            EndTime = DateTime.Now.AddMinutes(60),
            ProductId = FlashSalesTestData.Product1Id,
            ProductSkuId = FlashSalesTestData.ProductSku1Id,
            IsPublished = false
        };

        Product1.StoreId = Guid.NewGuid();
        await AppService.UpdateAsync(returnFlashSalePlanDto.Id, updateDto).ShouldThrowAsync<ProductIsNotInThisStoreException>();
        Product1.StoreId = FlashSalesTestData.Store1Id;

        Product1.InventoryStrategy = InventoryStrategy.ReduceAfterPlacing;
        await AppService.UpdateAsync(returnFlashSalePlanDto.Id, updateDto).ShouldThrowAsync<UnexpectedInventoryStrategyException>();

        Product1.StoreId = FlashSalesTestData.Store1Id;
        Product1.InventoryStrategy = InventoryStrategy.FlashSales;
    }

    [Fact]
    public async Task UpdateAsync_Should_Throw_Exception_When_Has_Result_And_Change_Product()
    {
        var createDto = new FlashSalePlanCreateDto()
        {
            StoreId = FlashSalesTestData.Store1Id,
            BeginTime = DateTime.Now,
            EndTime = DateTime.Now.AddMinutes(30),
            ProductId = FlashSalesTestData.Product1Id,
            ProductSkuId = FlashSalesTestData.ProductSku1Id,
            IsPublished = true
        };
        var returnFlashSalePlanDto = await AppService.CreateAsync(createDto);
        var updateDto = new FlashSalePlanUpdateDto()
        {
            BeginTime = DateTime.Now.AddMinutes(30),
            EndTime = DateTime.Now.AddMinutes(value: 60),
            ProductId = FlashSalesTestData.Product1Id,
            ProductSkuId = FlashSalesTestData.ProductSku2Id,
            IsPublished = false
        };
        await CreatePendingResultAsync(returnFlashSalePlanDto.Id, returnFlashSalePlanDto.StoreId, Guid.NewGuid());

        await AppService.UpdateAsync(returnFlashSalePlanDto.Id, updateDto).ShouldThrowAsync<RelatedFlashSaleResultsExistException>();
    }

    [Fact]
    public async Task DeleteAsync()
    {
        var createDto = new FlashSalePlanCreateDto()
        {
            StoreId = FlashSalesTestData.Store1Id,
            BeginTime = DateTime.Now,
            EndTime = DateTime.Now.AddMinutes(30),
            ProductId = FlashSalesTestData.Product1Id,
            ProductSkuId = FlashSalesTestData.ProductSku1Id,
            IsPublished = true
        };
        var returnFlashSalePlanDto = await AppService.CreateAsync(createDto);

        await AppService.DeleteAsync(returnFlashSalePlanDto.Id);
        await AppService.GetAsync(returnFlashSalePlanDto.Id).ShouldThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async Task DeleteAsync_Should_Throw_Exception_When_Has_Result()
    {
        var createDto = new FlashSalePlanCreateDto()
        {
            StoreId = FlashSalesTestData.Store1Id,
            BeginTime = DateTime.Now,
            EndTime = DateTime.Now.AddMinutes(30),
            ProductId = FlashSalesTestData.Product1Id,
            ProductSkuId = FlashSalesTestData.ProductSku1Id,
            IsPublished = true
        };
        var returnFlashSalePlanDto = await AppService.CreateAsync(createDto);
        await CreatePendingResultAsync(returnFlashSalePlanDto.Id, returnFlashSalePlanDto.StoreId, Guid.NewGuid());

        await AppService.DeleteAsync(returnFlashSalePlanDto.Id).ShouldThrowAsync<RelatedFlashSaleResultsExistException>();
    }

    [Fact]
    public async Task PreOrderAsync()
    {
        var options = GetRequiredService<IOptions<FlashSalesOptions>>().Value;
        var distributedCache = GetRequiredService<IDistributedCache<FlashSalePlanPreOrderCacheItem>>();
        var plan = await CreateFlashSalePlanAsync();
        var preOrderCacheKey = string.Format(FlashSalePlanAppService.PreOrderCacheKeyFormat, plan.Id, CurrentUser.Id);
        var hashToken = await GetRequiredService<IFlashSalePlanHasher>()
            .HashAsync(plan.LastModificationTime, Product1.LastModificationTime,
                ((ProductSkuDto)Product1.GetSkuById(plan.ProductSkuId)).LastModificationTime);

        var dto = await AppService.PreOrderAsync(plan.Id);

        dto.ExpiresInSeconds.ShouldBe(options.PreOrderExpires.TotalSeconds);
        var preOrderCacheItem = await distributedCache.GetAsync(preOrderCacheKey);
        preOrderCacheItem.ShouldNotBeNull();
        preOrderCacheItem.PlanId.ShouldBe(plan.Id);
        preOrderCacheItem.TenantId.ShouldBe(plan.TenantId);
        preOrderCacheItem.HashToken.ShouldBe(hashToken);
        preOrderCacheItem.ProductId.ShouldBe(plan.ProductId);
        preOrderCacheItem.ProductSkuId.ShouldBe(plan.ProductSkuId);
        preOrderCacheItem.InventoryProviderName = Product1.InventoryProviderName;
    }

    [Fact]
    public async Task PreOrderAsync_Should_Throw_Exception_When_Validate_PreOrder_Failed()
    {
        var plan = await CreateFlashSalePlanAsync();

        Product1.IsPublished = false;

        (await AppService.PreOrderAsync(plan.Id)
            .ShouldThrowAsync<BusinessException>())
            .Code.ShouldBe(FlashSalesErrorCodes.ProductIsNotPublished);

        Product1.IsPublished = true;
        Product1.InventoryStrategy = InventoryStrategy.ReduceAfterPlacing;

        await AppService.PreOrderAsync(plan.Id)
           .ShouldThrowAsync<UnexpectedInventoryStrategyException>();

        Product1.InventoryStrategy = InventoryStrategy.FlashSales;

        var plan2 = await CreateFlashSalePlanAsync(isPublished: false);
        await AppService.PreOrderAsync(plan2.Id)
           .ShouldThrowAsync<EntityNotFoundException>();

        var plan3 = await CreateFlashSalePlanAsync(timeRange: CreateTimeRange.Expired);
        (await AppService.PreOrderAsync(plan3.Id)
           .ShouldThrowAsync<BusinessException>())
           .Code.ShouldBe(FlashSalesErrorCodes.FlashSaleIsOver);
    }

    [Fact]
    public async Task OrderAsync()
    {
        var plan = await CreateFlashSalePlanAsync();
        var hashToken = await GetRequiredService<IFlashSalePlanHasher>()
            .HashAsync(plan.LastModificationTime, Product1.LastModificationTime,
                ((ProductSkuDto)Product1.GetSkuById(plan.ProductSkuId)).LastModificationTime);
        var orderFlashSalePlanInput = new OrderFlashSalePlanInput
        {
            CustomerRemark = "remark1"
        };
        orderFlashSalePlanInput.SetProperty("key1", "value1");
        await AppService.PreOrderAsync(plan.Id);

        var result = await AppService.OrderAsync(plan.Id, orderFlashSalePlanInput);

        result.IsSuccess.ShouldBe(true);
        await DistributedEventBus.Received().PublishAsync(Arg.Is<CreateFlashSaleResultEto>(eto =>
            eto.TenantId == plan.TenantId &&
            eto.UserId == CurrentUser.GetId() &&
            eto.HashToken == hashToken &&
            eto.CustomerRemark == orderFlashSalePlanInput.CustomerRemark &&
            eto.Plan != null &&
            eto.Plan.Id == plan.Id &&
            eto.Plan.TenantId == plan.TenantId &&
            eto.Plan.StoreId == plan.StoreId &&
            eto.Plan.BeginTime == plan.BeginTime &&
            eto.Plan.EndTime == plan.EndTime &&
            eto.Plan.ProductId == plan.ProductId &&
            eto.Plan.ProductSkuId == plan.ProductSkuId &&
            eto.Plan.IsPublished == plan.IsPublished &&
            eto.ExtraProperties.ContainsKey("key1") &&
            eto.ExtraProperties["key1"].ToString() == "value1"
        ), false, false);
    }

    [Fact]
    public async Task OrderAsync_Throw_Exception_When_Not_PreOrder()
    {
        var plan = await CreateFlashSalePlanAsync();
        var orderFlashSalePlanInput = new OrderFlashSalePlanInput();

        (await AppService.OrderAsync(plan.Id, orderFlashSalePlanInput)).ErrorCode
            .ShouldBe(FlashSalesErrorCodes.PreOrderExpired);
    }

    [Fact]
    public async Task OrderAsync_Throw_Exception_When_FlashSaleNotStarted()
    {
        var plan = await CreateFlashSalePlanAsync(timeRange: CreateTimeRange.NotStart);
        var orderFlashSalePlanInput = new OrderFlashSalePlanInput();
        await AppService.PreOrderAsync(plan.Id);

        (await AppService.OrderAsync(plan.Id, orderFlashSalePlanInput))
            .ErrorCode.ShouldBe(FlashSalesErrorCodes.FlashSaleNotStarted);
    }

    [Fact]
    public async Task OrderAsync_Throw_Exception_When_FlashSaleIsOver()
    {
        var plan = await CreateFlashSalePlanAsync(timeRange: CreateTimeRange.WillBeExpired);
        var orderFlashSalePlanInput = new OrderFlashSalePlanInput();
        await AppService.PreOrderAsync(plan.Id);

        await Task.Delay(TimeSpan.FromSeconds(1.2));

        (await AppService.OrderAsync(plan.Id, orderFlashSalePlanInput))
            .ErrorCode.ShouldBe(FlashSalesErrorCodes.FlashSaleIsOver);
    }

    [Fact]
    public async Task OrderAsync_Throw_Exception_When_BusyToCreateFlashSaleOrder()
    {
        var plan = await CreateFlashSalePlanAsync();
        var orderFlashSalePlanInput = new OrderFlashSalePlanInput();
        await AppService.PreOrderAsync(plan.Id);
        var distributedLock = GetRequiredService<IAbpDistributedLock>();
        var lockKey = $"create-flash-sale-order-{plan.Id}-{CurrentUser.GetId()}";

        await using var handle = await distributedLock.TryAcquireAsync(lockKey);

        (await AppService.OrderAsync(plan.Id, orderFlashSalePlanInput))
            .ErrorCode.ShouldBe(FlashSalesErrorCodes.BusyToCreateFlashSaleOrder);
    }

    [Fact]
    public async Task OrderAsync_Throw_Exception_When_Exist_FlashSaleCurrentResultCache()
    {
        var plan = await CreateFlashSalePlanAsync();
        var orderFlashSalePlanInput = new OrderFlashSalePlanInput();
        await AppService.PreOrderAsync(plan.Id);
        var flashSaleCurrentResultCache = GetRequiredService<IFlashSaleCurrentResultCache>();
        var userId = CurrentUser.GetId();
        await flashSaleCurrentResultCache.SetAsync(plan.Id, userId,
            new FlashSaleCurrentResultCacheItem
            {
                TenantId = CurrentTenant.Id,
                ResultDto = new FlashSaleResultDto
                {
                    Id = Guid.NewGuid(),
                    StoreId = plan.StoreId,
                    PlanId = plan.Id,
                    UserId = userId
                }
            });

        (await AppService.OrderAsync(plan.Id, orderFlashSalePlanInput))
            .ErrorCode.ShouldBe(FlashSalesErrorCodes.DuplicateFlashSalesOrder);
    }

    [Fact]
    public async Task OrderAsync_Return_False_When_TryReduceInventory_Failed()
    {
        var plan = await CreateFlashSalePlanAsync();
        var orderFlashSalePlanInput = new OrderFlashSalePlanInput();
        await AppService.PreOrderAsync(plan.Id);

        FakeFlashSaleInventoryManager.ShouldReduceSuccess = false;

        var result = await AppService.OrderAsync(plan.Id, orderFlashSalePlanInput);
        result.IsSuccess.ShouldBe(false);
    }

    [Fact]
    public async Task OrderAsync_Return_False_When_Exist_Not_Failed_Result()
    {
        var plan = await CreateFlashSalePlanAsync();
        var orderFlashSalePlanInput = new OrderFlashSalePlanInput();
        await AppService.PreOrderAsync(plan.Id);
        var userId = GetRequiredService<ICurrentUser>().GetId();

        var existingResult = await CreatePendingResultAsync(plan.Id, plan.StoreId, userId);

        Guid? newResultId = null;
        DistributedEventBus.PublishAsync<CreateFlashSaleResultEto>(null, false, false).ReturnsForAnyArgs(async info =>
        {
            var eto = info.Arg<CreateFlashSaleResultEto>();
            newResultId = eto.ResultId;
            var handler = ServiceProvider.GetRequiredService<CreateFlashSaleResultEventHandler>();
            await handler.HandleEventAsync(eto);
        });

        await AppService.OrderAsync(plan.Id, orderFlashSalePlanInput);

        await DistributedEventBus.Received()
            .PublishAsync(Arg.Is<CreateFlashSaleResultEto>(eto => eto.ResultId == newResultId), false, false);

        var flashSaleResultAppService = ServiceProvider.GetRequiredService<IFlashSaleResultAppService>();
        (await flashSaleResultAppService.GetCurrentAsync(plan.Id)).Id.ShouldBe(existingResult.Id);
    }
}
