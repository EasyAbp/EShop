using System;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;

public interface IFlashSalesResultAppService :
    IReadOnlyAppService<
        FlashSalesResultDto,
        Guid,
        FlashSalesResultGetListInput>
{

}
