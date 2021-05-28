using System;
using EasyAbp.EShop.Inventory.Instocks.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.Instocks
{
    public interface IInstockAppService :
        ICrudAppService< 
            InstockDto, 
            Guid,
            GetInstockListInput,
            InstockCreateDto,
            InstockUpdateDto>
    {

    }
}