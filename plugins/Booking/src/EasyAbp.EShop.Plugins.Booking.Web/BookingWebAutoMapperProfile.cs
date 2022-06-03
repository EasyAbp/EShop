using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAsset.ViewModels;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAssetPeriod.ViewModels;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using AutoMapper;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssetCategories.ProductAssetCategory.ViewModels;
using EasyAbp.EShop.Plugins.Booking.StoreAssetCategories.Dtos;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssetCategories.ProductAssetCategoryPeriod.ViewModels;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.StoreAssetCategories.StoreAssetCategory.ViewModels;

namespace EasyAbp.EShop.Plugins.Booking.Web;

public class BookingWebAutoMapperProfile : Profile
{
    public BookingWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ProductAssetDto, EditProductAssetViewModel>();
        CreateMap<CreateProductAssetViewModel, CreateProductAssetDto>();
        CreateMap<EditProductAssetViewModel, UpdateProductAssetDto>();

        CreateMap<ProductAssetPeriodDto, EditProductAssetPeriodViewModel>();
        CreateMap<CreateProductAssetPeriodViewModel, CreateProductAssetPeriodDto>();
        CreateMap<EditProductAssetPeriodViewModel, UpdateProductAssetPeriodDto>();

        CreateMap<ProductAssetCategoryDto, EditProductAssetCategoryViewModel>();
        CreateMap<CreateProductAssetCategoryViewModel, CreateProductAssetCategoryDto>();
        CreateMap<EditProductAssetCategoryViewModel, UpdateProductAssetCategoryDto>();

        CreateMap<ProductAssetCategoryPeriodDto, EditProductAssetCategoryPeriodViewModel>();
        CreateMap<CreateProductAssetCategoryPeriodViewModel, CreateProductAssetCategoryPeriodDto>();
        CreateMap<EditProductAssetCategoryPeriodViewModel, UpdateProductAssetCategoryPeriodDto>();

        CreateMap<StoreAssetCategoryDto, CreateEditStoreAssetCategoryViewModel>();
        CreateMap<CreateEditStoreAssetCategoryViewModel, CreateUpdateStoreAssetCategoryDto>();
    }
}
