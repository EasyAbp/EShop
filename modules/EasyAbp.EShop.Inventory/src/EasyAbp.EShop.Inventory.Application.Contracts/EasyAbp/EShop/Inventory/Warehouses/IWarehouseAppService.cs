using System;
using EasyAbp.EShop.Inventory.Warehouses.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.Warehouses
{
    public interface IWarehouseAppService :
        ICrudAppService< 
            WarehouseDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            WarehouseCreateDto,
            WarehouseUpdateDto>
    {

    }
}