using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using Shouldly;
using Volo.Abp.Guids;
using Xunit;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public class CreateFlashSaleOrderCompleteEventHandlerTests : FlashSalesDomainTestBase
{
    protected IFlashSaleResultRepository FlashSaleResultRepository { get; }
    protected CreateFlashSaleOrderCompleteEventHandler CreateFlashSaleOrderCompleteEventHandler { get; }
    protected IGuidGenerator GuidGenerator { get; }

    public CreateFlashSaleOrderCompleteEventHandlerTests()
    {
        FlashSaleResultRepository = GetRequiredService<IFlashSaleResultRepository>();
        CreateFlashSaleOrderCompleteEventHandler = GetRequiredService<CreateFlashSaleOrderCompleteEventHandler>();
        GuidGenerator = GetRequiredService<IGuidGenerator>();
    }

    [Fact]
    public async Task HandleEventAsync_When_Create_Order_Success()
    {
        var existFlashResult = await CreateFlashSaleResultAsync();
        var createFlashSaleOrderCompleteEto = new CreateFlashSaleOrderCompleteEto()
        {
            TenantId = existFlashResult.TenantId,
            PendingResultId = existFlashResult.Id,
            Success = true,
            StoreId = existFlashResult.StoreId,
            PlanId = existFlashResult.PlanId,
            OrderId = GuidGenerator.Create(),
            Reason = null,
            UserId = existFlashResult.UserId,
        };

        await CreateFlashSaleOrderCompleteEventHandler.HandleEventAsync(createFlashSaleOrderCompleteEto);

        var flashResult = await FlashSaleResultRepository.GetAsync(existFlashResult.Id);
        flashResult.Status.ShouldBe(FlashSaleResultStatus.Successful);
        flashResult.OrderId.ShouldBe(createFlashSaleOrderCompleteEto.OrderId);
        flashResult.Reason.ShouldBe(null);
    }

    [Fact]
    public async Task HandleEventAsync_When_Create_Order_Failed()
    {
        var existFlashResult = await CreateFlashSaleResultAsync();
        var createFlashSaleOrderCompleteEto = new CreateFlashSaleOrderCompleteEto()
        {
            TenantId = existFlashResult.TenantId,
            PendingResultId = existFlashResult.Id,
            Success = false,
            StoreId = FlashSalesTestData.Store1Id,
            PlanId = existFlashResult.PlanId,
            OrderId = null,
            Reason = "Failed reason",
            UserId = existFlashResult.UserId,
        };

        await CreateFlashSaleOrderCompleteEventHandler.HandleEventAsync(createFlashSaleOrderCompleteEto);

        var flashResult = await FlashSaleResultRepository.GetAsync(existFlashResult.Id);
        flashResult.Status.ShouldBe(FlashSaleResultStatus.Failed);
        flashResult.OrderId.ShouldBe(null);
        flashResult.Reason.ShouldBe("Failed reason");
    }

    public async Task<FlashSaleResult> CreateFlashSaleResultAsync()
    {
        return await WithUnitOfWorkAsync(async () =>
        {
            var flashSaleResult = new FlashSaleResult(
                GuidGenerator.Create(),
                null,
                FlashSalesTestData.Store1Id,
                FlashSalesTestData.Plan1Id, 
                GuidGenerator.Create());
            await FlashSaleResultRepository.InsertAsync(flashSaleResult);

            return flashSaleResult;
        });
    }
}
