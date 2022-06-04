using System;
using EasyAbp.EShop.Plugins.Booking.StoreAssetCategories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Booking.StoreAssetCategories
{
    public interface IStoreAssetCategoryAppService :
        ICrudAppService< 
            StoreAssetCategoryDto, 
            Guid, 
            GetStoreAssetCategoryListDto,
            CreateUpdateStoreAssetCategoryDto,
            CreateUpdateStoreAssetCategoryDto>
    {

    }
}