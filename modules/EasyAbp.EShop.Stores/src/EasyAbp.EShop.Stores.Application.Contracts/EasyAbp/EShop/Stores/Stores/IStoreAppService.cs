using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Stores.Stores
{
    public interface IStoreAppService :
        ICrudAppService< 
            StoreDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateStoreDto,
            CreateUpdateStoreDto>
    {
        Task<StoreDto> GetDefaultAsync();
    }
}