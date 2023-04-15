using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public class CreateFlashSaleResultEventHandlerTests : FlashSalesApplicationTestBase
{
    protected IFlashSaleResultAppService FlashSaleResultAppService { get; }
    protected CreateFlashSaleResultEventHandler CreateFlashSaleResultEventHandler { get; }
    protected IFlashSaleInventoryManager FlashSaleInventoryManager { get; }

    public CreateFlashSaleResultEventHandlerTests()
    {
        FlashSaleResultAppService = GetRequiredService<IFlashSaleResultAppService>();
        CreateFlashSaleResultEventHandler = GetRequiredService<CreateFlashSaleResultEventHandler>();
        FlashSaleInventoryManager = GetRequiredService<IFlashSaleInventoryManager>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        var flashSaleInventoryManager = Substitute.For<IFlashSaleInventoryManager>();
        services.Replace(ServiceDescriptor.Singleton(flashSaleInventoryManager));

        base.AfterAddApplication(services);
    }

    [Fact]
    public async Task Should_Create_Result()
    {
        var reducedInventoryTime = DateTime.Now;
        var createFlashSaleResultEto = await CreateCreateFlashSaleResultEtoAsync(reducedInventoryTime);

        await CreateFlashSaleResultEventHandler.HandleEventAsync(createFlashSaleResultEto);

        var flashResult = await FlashSaleResultRepository.GetAsync(FlashSalesTestData.Result1Id);
        flashResult.Status.ShouldBe(FlashSaleResultStatus.Pending);
        flashResult.OrderId.ShouldBeNull();
        flashResult.Reason.ShouldBe(null);
        flashResult.ReducedInventoryTime.ShouldBe(reducedInventoryTime);

        (await FlashSaleResultAppService.GetCurrentAsync(createFlashSaleResultEto.Plan.Id)).Id.ShouldBe(flashResult.Id);
    }

    [Fact]
    public async Task Should_Not_Create_Result_If_Duplicate()
    {
        var existFlashResult1 = await CreateFlashSaleResultAsync();
        var createFlashSaleResultEto1 = await CreateCreateFlashSaleResultEtoAsync(DateTime.Now);
        var planId = createFlashSaleResultEto1.Plan.Id;

        existFlashResult1.MarkAsFailed("some reason");
        await FlashSaleResultRepository.UpdateAsync(existFlashResult1, true);

        (await FlashSaleResultAppService.GetCurrentAsync(planId)).Id.ShouldBe(existFlashResult1.Id);

        await CreateFlashSaleResultEventHandler.HandleEventAsync(createFlashSaleResultEto1);

        await FlashSaleInventoryManager.DidNotReceive()
            .TryRollBackInventoryAsync(createFlashSaleResultEto1.TenantId,
                createFlashSaleResultEto1.ProductInventoryProviderName, createFlashSaleResultEto1.Plan.StoreId,
                createFlashSaleResultEto1.Plan.ProductId, createFlashSaleResultEto1.Plan.ProductSkuId);

        var flashResultList = await FlashSaleResultRepository.GetListAsync();
        flashResultList.Count.ShouldBe(2);
        flashResultList.ShouldContain(x => x.Id == existFlashResult1.Id);

        (await FlashSaleResultAppService.GetCurrentAsync(planId)).Id.ShouldNotBe(existFlashResult1.Id);

        var existFlashResult2 = flashResultList.First(x => x.Id != existFlashResult1.Id);
        var createFlashSaleResultEto2 = await CreateCreateFlashSaleResultEtoAsync(DateTime.Now);

        await CreateFlashSaleResultEventHandler.HandleEventAsync(createFlashSaleResultEto2);

        await FlashSaleInventoryManager.Received()
            .TryRollBackInventoryAsync(createFlashSaleResultEto2.TenantId,
                createFlashSaleResultEto2.ProductInventoryProviderName, createFlashSaleResultEto2.Plan.StoreId,
                createFlashSaleResultEto2.Plan.ProductId, createFlashSaleResultEto2.Plan.ProductSkuId);

        flashResultList = await FlashSaleResultRepository.GetListAsync();
        flashResultList.Count.ShouldBe(2);
        flashResultList.ShouldContain(x => x.Id == existFlashResult1.Id);
        flashResultList.ShouldContain(x => x.Id == existFlashResult2.Id);

        (await FlashSaleResultAppService.GetCurrentAsync(planId)).Id.ShouldBe(existFlashResult2.Id);

        existFlashResult2.MarkAsSuccessful(GuidGenerator.Create());
        await FlashSaleResultRepository.UpdateAsync(existFlashResult2, true);

        await CreateFlashSaleResultEventHandler.HandleEventAsync(createFlashSaleResultEto2);

        await FlashSaleInventoryManager.Received()
            .TryRollBackInventoryAsync(createFlashSaleResultEto2.TenantId,
                createFlashSaleResultEto2.ProductInventoryProviderName, createFlashSaleResultEto2.Plan.StoreId,
                createFlashSaleResultEto2.Plan.ProductId, createFlashSaleResultEto2.Plan.ProductSkuId);

        flashResultList = await FlashSaleResultRepository.GetListAsync();
        flashResultList.Count.ShouldBe(2);
        flashResultList.ShouldContain(x => x.Id == existFlashResult1.Id);
        flashResultList.ShouldContain(x => x.Id == existFlashResult2.Id);

        (await FlashSaleResultAppService.GetCurrentAsync(planId)).Id.ShouldBe(existFlashResult2.Id);
    }

    protected Task<CreateFlashSaleResultEto> CreateCreateFlashSaleResultEtoAsync(DateTime reducedInventoryTime)
    {
        return Task.FromResult(new CreateFlashSaleResultEto
        {
            ResultId = FlashSalesTestData.Result1Id,
            UserId = CurrentUser.GetId(),
            ReducedInventoryTime = reducedInventoryTime,
            Plan = new FlashSalePlanEto
            {
                Id = FlashSalesTestData.Plan1Id,
                TenantId = null,
                StoreId = FlashSalesTestData.Store1Id,
                BeginTime = reducedInventoryTime,
                EndTime = DateTime.Now.AddMinutes(30),
                ProductId = FlashSalesTestData.Product1Id,
                ProductSkuId = FlashSalesTestData.ProductSku1Id,
                IsPublished = true
            },
            HashToken = "My Hash Token"
        });
    }

    protected async Task<FlashSaleResult> CreateFlashSaleResultAsync()
    {
        return await WithUnitOfWorkAsync(async () =>
        {
            var flashSaleResult = new FlashSaleResult(
                GuidGenerator.Create(),
                null,
                FlashSalesTestData.Store1Id,
                FlashSalesTestData.Plan1Id, 
                CurrentUser.GetId(),
                DateTime.Now);
            await FlashSaleResultRepository.InsertAsync(flashSaleResult);

            return flashSaleResult;
        });
    }
}
