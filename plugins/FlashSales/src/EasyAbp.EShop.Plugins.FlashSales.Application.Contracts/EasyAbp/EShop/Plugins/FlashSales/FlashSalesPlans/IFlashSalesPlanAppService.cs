using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public interface IFlashSalesPlanAppService :
    ICrudAppService<
        FlashSalesPlanDto,
        Guid,
        FlashSalesPlanGetListInput,
        FlashSalesPlanCreateDto,
        FlashSalesPlanUpdateDto>
{
    Task PreOrderAsync(Guid id);

    Task CheckPreOrderAsync(Guid id);

    Task CreateOrderAsync(Guid id, CreateOrderInput input);
}
