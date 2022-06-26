using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.Permissions;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;

public class FlashSalesResultAppService :
    ReadOnlyAppService<FlashSalesResult, FlashSalesResultDto, Guid, FlashSalesResultGetListInput>,
    IFlashSalesResultAppService
{
    protected override string GetPolicyName { get; set; }
    protected override string GetListPolicyName { get; set; }

    protected IFlashSalesResultRepository FlashSalesResultRepository { get; }

    public FlashSalesResultAppService(IFlashSalesResultRepository flashSalesResultRepository) : base(flashSalesResultRepository)
    {
    }

    public override async Task<FlashSalesResultDto> GetAsync(Guid id)
    {
        await CheckGetPolicyAsync();

        var flashSalesPlan = await GetEntityByIdAsync(id);

        if (flashSalesPlan.UserId != CurrentUser.Id)
        {
            await CheckPolicyAsync(FlashSalesPermissions.FlashSalesResult.Manage);
        }

        return await MapToGetOutputDtoAsync(flashSalesPlan);
    }

    protected override async Task<IQueryable<FlashSalesResult>> CreateFilteredQueryAsync(FlashSalesResultGetListInput input)
    {
        if (input.UserId != CurrentUser.Id)
        {
            await CheckPolicyAsync(FlashSalesPermissions.FlashSalesResult.Manage);
        }

        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.StoreId.HasValue, x => x.StoreId == input.StoreId.Value)
            .WhereIf(input.PlanId.HasValue, x => x.PlanId == input.PlanId.Value)
            .WhereIf(input.Status.HasValue, x => x.Status == input.Status.Value)
            .WhereIf(input.UserId.HasValue, x => x.UserId == input.UserId.Value)
            .WhereIf(input.OrderId.HasValue, x => x.OrderId == input.OrderId.Value);
    }
}
