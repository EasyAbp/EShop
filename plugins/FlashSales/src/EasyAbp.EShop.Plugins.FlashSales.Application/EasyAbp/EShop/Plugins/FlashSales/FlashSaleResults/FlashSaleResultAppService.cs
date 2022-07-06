using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.Permissions;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;

public class FlashSaleResultAppService :
    ReadOnlyAppService<FlashSaleResult, FlashSaleResultDto, Guid, FlashSaleResultGetListInput>,
    IFlashSaleResultAppService
{
    protected override string GetPolicyName { get; set; }
    protected override string GetListPolicyName { get; set; }

    protected IFlashSaleResultRepository FlashSaleResultRepository { get; }

    public FlashSaleResultAppService(IFlashSaleResultRepository flashSalesResultRepository) : base(flashSalesResultRepository)
    {
    }

    public override async Task<FlashSaleResultDto> GetAsync(Guid id)
    {
        await CheckGetPolicyAsync();

        var flashSalesPlan = await GetEntityByIdAsync(id);

        if (flashSalesPlan.UserId != CurrentUser.Id)
        {
            await CheckPolicyAsync(FlashSalesPermissions.FlashSaleResult.Manage);
        }

        return await MapToGetOutputDtoAsync(flashSalesPlan);
    }

    protected override async Task<IQueryable<FlashSaleResult>> CreateFilteredQueryAsync(FlashSaleResultGetListInput input)
    {
        if (input.UserId != CurrentUser.Id)
        {
            await CheckPolicyAsync(FlashSalesPermissions.FlashSaleResult.Manage);
        }

        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.StoreId.HasValue, x => x.StoreId == input.StoreId.Value)
            .WhereIf(input.PlanId.HasValue, x => x.PlanId == input.PlanId.Value)
            .WhereIf(input.Status.HasValue, x => x.Status == input.Status.Value)
            .WhereIf(input.UserId.HasValue, x => x.UserId == input.UserId.Value)
            .WhereIf(input.OrderId.HasValue, x => x.OrderId == input.OrderId.Value);
    }
}
