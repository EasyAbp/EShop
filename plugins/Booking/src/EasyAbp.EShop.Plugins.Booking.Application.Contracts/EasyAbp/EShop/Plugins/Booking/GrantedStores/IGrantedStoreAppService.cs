using System;
using EasyAbp.EShop.Plugins.Booking.GrantedStores.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Booking.GrantedStores
{
    public interface IGrantedStoreAppService :
        ICrudAppService< 
            GrantedStoreDto, 
            Guid, 
            GetGrantedStoreListDto,
            CreateUpdateGrantedStoreDto,
            CreateUpdateGrantedStoreDto>
    {

    }
}