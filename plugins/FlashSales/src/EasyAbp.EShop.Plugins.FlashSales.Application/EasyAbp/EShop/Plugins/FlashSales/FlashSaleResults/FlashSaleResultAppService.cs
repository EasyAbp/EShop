using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.Permissions;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;

public class FlashSaleResultAppService :
    MultiStoreCrudAppService<FlashSaleResult, FlashSaleResultDto, Guid, FlashSaleResultGetListInput>,
    IFlashSaleResultAppService
{
    protected override string CrossStorePolicyName { get; set; } = FlashSalesPermissions.FlashSaleResult.CrossStore;
    protected override string GetPolicyName { get; set; }
    protected override string GetListPolicyName { get; set; }

    protected IFlashSaleResultRepository FlashSaleResultRepository { get; }

    public FlashSaleResultAppService(IFlashSaleResultRepository flashSalesResultRepository) : base(flashSalesResultRepository)
    {
    }

    public override async Task<FlashSaleResultDto> GetAsync(Guid id)
    {
        var flashSalesResult = await GetEntityByIdAsync(id);

        await CheckMultiStorePolicyAsync(flashSalesResult.StoreId, GetPolicyName);

        if (flashSalesResult.UserId != CurrentUser.Id)
        {
            await CheckMultiStorePolicyAsync(flashSalesResult.StoreId, FlashSalesPermissions.FlashSaleResult.Manage);
        }

        return await MapToGetOutputDtoAsync(flashSalesResult);
    }

    public override async Task<PagedResultDto<FlashSaleResultDto>> GetListAsync(FlashSaleResultGetListInput input)
    {
        await CheckMultiStorePolicyAsync(input.StoreId, GetListPolicyName);

        return await base.GetListAsync(input);
    }

    protected override async Task<IQueryable<FlashSaleResult>> CreateFilteredQueryAsync(FlashSaleResultGetListInput input)
    {
        if (input.UserId != CurrentUser.Id)
        {
            await CheckMultiStorePolicyAsync(input.StoreId, FlashSalesPermissions.FlashSaleResult.Manage);
        }

        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.StoreId.HasValue, x => x.StoreId == input.StoreId.Value)
            .WhereIf(input.PlanId.HasValue, x => x.PlanId == input.PlanId.Value)
            .WhereIf(input.Status.HasValue, x => x.Status == input.Status.Value)
            .WhereIf(input.UserId.HasValue, x => x.UserId == input.UserId.Value)
            .WhereIf(input.OrderId.HasValue, x => x.OrderId == input.OrderId.Value);
    }
}
