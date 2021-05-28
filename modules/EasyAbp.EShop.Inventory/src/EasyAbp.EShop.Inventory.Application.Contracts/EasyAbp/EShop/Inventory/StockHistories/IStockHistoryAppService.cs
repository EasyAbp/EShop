using System;
using EasyAbp.EShop.Inventory.StockHistories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.StockHistories
{
    public interface IStockHistoryAppService :
        ICrudAppService< 
            StockHistoryDto, 
            Guid,
            GetStockHistoryListInput,
            StockHistoryCreateDto,
            StockHistoryUpdateDto>
    {

    }
}