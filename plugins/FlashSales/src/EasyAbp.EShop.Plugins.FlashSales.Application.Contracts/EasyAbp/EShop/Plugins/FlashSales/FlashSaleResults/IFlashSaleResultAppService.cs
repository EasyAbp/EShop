using System;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public interface IFlashSaleResultAppService :
    IReadOnlyAppService<
        FlashSaleResultDto,
        Guid,
        FlashSaleResultGetListInput>
{

}
