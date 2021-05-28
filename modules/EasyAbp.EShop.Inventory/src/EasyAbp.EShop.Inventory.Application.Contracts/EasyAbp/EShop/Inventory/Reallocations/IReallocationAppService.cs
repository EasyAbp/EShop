using System;
using EasyAbp.EShop.Inventory.Reallocations.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.Reallocations
{
    public interface IReallocationAppService :
        ICrudAppService< 
            ReallocationDto, 
            Guid,
            GetReallocationListInput,
            ReallocationCreateDto,
            ReallocationUpdateDto>
    {

    }
}