using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.Permissions;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public class FlashSaleResultAppService :
    MultiStoreCrudAppService<FlashSaleResult, FlashSaleResultDto, Guid, FlashSaleResultGetListInput>,
    IFlashSaleResultAppService
{
    protected override string CrossStorePolicyName { get; set; } = FlashSalesPermissions.FlashSaleResult.CrossStore;
    protected override string GetPolicyName { get; set; } = FlashSalesPermissions.FlashSaleResult.Default;
    protected override string GetListPolicyName { get; set; } = FlashSalesPermissions.FlashSaleResult.Default;

    public FlashSaleResultAppService(IFlashSaleResultRepository flashSaleResultRepository) : base(flashSaleResultRepository)
    {
    }

    public override async Task<FlashSaleResultDto> GetAsync(Guid id)
    {
        var flashSaleResult = await GetEntityByIdAsync(id);

        if (flashSaleResult.UserId == CurrentUser.Id)
        {
            await CheckGetPolicyAsync();
        }
        else
        {
            await CheckMultiStorePolicyAsync(flashSaleResult.StoreId, FlashSalesPermissions.FlashSaleResult.Manage);
        }

        return await MapToGetOutputDtoAsync(flashSaleResult);
    }

    public override async Task<PagedResultDto<FlashSaleResultDto>> GetListAsync(FlashSaleResultGetListInput input)
    {
        if (input.UserId.HasValue && input.UserId == CurrentUser.Id)
        {
            await CheckGetListPolicyAsync();
        }
        else
        {
            await CheckMultiStorePolicyAsync(input.StoreId, GetListPolicyName);
        }

        return await base.GetListAsync(input);
    }

    protected override async Task<IQueryable<FlashSaleResult>> CreateFilteredQueryAsync(FlashSaleResultGetListInput input)
    {
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.StoreId.HasValue, x => x.StoreId == input.StoreId.Value)
            .WhereIf(input.PlanId.HasValue, x => x.PlanId == input.PlanId.Value)
            .WhereIf(input.Status.HasValue, x => x.Status == input.Status.Value)
            .WhereIf(input.UserId.HasValue, x => x.UserId == input.UserId.Value)
            .WhereIf(input.OrderId.HasValue, x => x.OrderId == input.OrderId.Value);
    }
}
