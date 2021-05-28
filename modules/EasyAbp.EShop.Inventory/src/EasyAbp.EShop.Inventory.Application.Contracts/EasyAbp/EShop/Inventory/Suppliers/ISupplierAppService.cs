using System;
using EasyAbp.EShop.Inventory.Suppliers.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.Suppliers
{
    public interface ISupplierAppService :
        ICrudAppService< 
            SupplierDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            SupplierCreateDto,
            SupplierUpdateDto>
    {

    }
}