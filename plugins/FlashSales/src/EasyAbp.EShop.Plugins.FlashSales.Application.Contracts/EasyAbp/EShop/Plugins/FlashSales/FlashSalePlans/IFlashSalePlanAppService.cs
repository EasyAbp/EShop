using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public interface IFlashSalePlanAppService :
    ICrudAppService<
        FlashSalePlanDto,
        Guid,
        FlashSalePlanGetListInput,
        FlashSalePlanCreateDto,
        FlashSalePlanUpdateDto>
{
    Task<FlashSalePlanPreOrderDto> PreOrderAsync(Guid id);

    Task<FlashSaleOrderResultDto> OrderAsync(Guid id, OrderFlashSalePlanInput flashSalePlanInput);
}
