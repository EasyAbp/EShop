using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using AutoMapper;

namespace EasyAbp.EShop.Plugins.Booking;

public class BookingApplicationAutoMapperProfile : Profile
{
    public BookingApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ProductAsset, ProductAssetDto>();
        CreateMap<CreateProductAssetDto, ProductAsset>(MemberList.Source);
        CreateMap<UpdateProductAssetDto, ProductAsset>(MemberList.Source);

        CreateMap<ProductAssetPeriod, ProductAssetPeriodDto>();
        CreateMap<CreateProductAssetPeriodDto, ProductAssetPeriod>(MemberList.Source);
        CreateMap<UpdateProductAssetPeriodDto, ProductAssetPeriod>(MemberList.Source);

        CreateMap<ProductAssetCategory, ProductAssetCategoryDto>();
        CreateMap<CreateProductAssetCategoryDto, ProductAssetCategory>(MemberList.Source);
        CreateMap<UpdateProductAssetCategoryDto, ProductAssetCategory>(MemberList.Source);

        CreateMap<ProductAssetCategoryPeriod, ProductAssetCategoryPeriodDto>();
        CreateMap<CreateProductAssetCategoryPeriodDto, ProductAssetCategoryPeriod>(MemberList.Source);
        CreateMap<UpdateProductAssetCategoryPeriodDto, ProductAssetCategoryPeriod>(MemberList.Source);
    }
}