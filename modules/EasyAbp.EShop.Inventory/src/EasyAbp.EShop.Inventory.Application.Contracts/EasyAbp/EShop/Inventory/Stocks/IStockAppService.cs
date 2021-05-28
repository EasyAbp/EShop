using System;
using EasyAbp.EShop.Inventory.Stocks.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.Stocks
{
    public interface IStockAppService :
        ICrudAppService< 
            StockDto, 
            Guid,
            GetStockListInput,
            StockCreateDto,
            StockUpdateDto>
    {

    }
}