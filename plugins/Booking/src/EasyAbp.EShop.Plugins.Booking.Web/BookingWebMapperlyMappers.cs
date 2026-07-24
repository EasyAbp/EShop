using EasyAbp.EShop.Plugins.Booking.GrantedStores.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.GrantedStores.GrantedStore.ViewModels;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssetCategories.ProductAssetCategory.ViewModels;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssetCategories.ProductAssetCategoryPeriod.ViewModels;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAsset.ViewModels;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAssetPeriod.ViewModels;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Plugins.Booking.Web
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductAssetDtoToEditProductAssetViewModelMapper : MapperBase<ProductAssetDto, EditProductAssetViewModel>
    {
        public override partial EditProductAssetViewModel Map(ProductAssetDto source);

        public override partial void Map(ProductAssetDto source, EditProductAssetViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateProductAssetViewModelToCreateProductAssetDtoMapper : MapperBase<CreateProductAssetViewModel, CreateProductAssetDto>
    {
        public override partial CreateProductAssetDto Map(CreateProductAssetViewModel source);

        public override partial void Map(CreateProductAssetViewModel source, CreateProductAssetDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class EditProductAssetViewModelToUpdateProductAssetDtoMapper : MapperBase<EditProductAssetViewModel, UpdateProductAssetDto>
    {
        public override partial UpdateProductAssetDto Map(EditProductAssetViewModel source);

        public override partial void Map(EditProductAssetViewModel source, UpdateProductAssetDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductAssetPeriodDtoToEditProductAssetPeriodViewModelMapper : MapperBase<ProductAssetPeriodDto, EditProductAssetPeriodViewModel>
    {
        public override partial EditProductAssetPeriodViewModel Map(ProductAssetPeriodDto source);

        public override partial void Map(ProductAssetPeriodDto source, EditProductAssetPeriodViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateProductAssetPeriodViewModelToCreateProductAssetPeriodDtoMapper : MapperBase<CreateProductAssetPeriodViewModel, CreateProductAssetPeriodDto>
    {
        public override partial CreateProductAssetPeriodDto Map(CreateProductAssetPeriodViewModel source);

        public override partial void Map(CreateProductAssetPeriodViewModel source, CreateProductAssetPeriodDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class EditProductAssetPeriodViewModelToUpdateProductAssetPeriodDtoMapper : MapperBase<EditProductAssetPeriodViewModel, UpdateProductAssetPeriodDto>
    {
        public override partial UpdateProductAssetPeriodDto Map(EditProductAssetPeriodViewModel source);

        public override partial void Map(EditProductAssetPeriodViewModel source, UpdateProductAssetPeriodDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductAssetCategoryDtoToEditProductAssetCategoryViewModelMapper : MapperBase<ProductAssetCategoryDto, EditProductAssetCategoryViewModel>
    {
        public override partial EditProductAssetCategoryViewModel Map(ProductAssetCategoryDto source);

        public override partial void Map(ProductAssetCategoryDto source, EditProductAssetCategoryViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateProductAssetCategoryViewModelToCreateProductAssetCategoryDtoMapper : MapperBase<CreateProductAssetCategoryViewModel, CreateProductAssetCategoryDto>
    {
        public override partial CreateProductAssetCategoryDto Map(CreateProductAssetCategoryViewModel source);

        public override partial void Map(CreateProductAssetCategoryViewModel source, CreateProductAssetCategoryDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class EditProductAssetCategoryViewModelToUpdateProductAssetCategoryDtoMapper : MapperBase<EditProductAssetCategoryViewModel, UpdateProductAssetCategoryDto>
    {
        public override partial UpdateProductAssetCategoryDto Map(EditProductAssetCategoryViewModel source);

        public override partial void Map(EditProductAssetCategoryViewModel source, UpdateProductAssetCategoryDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductAssetCategoryPeriodDtoToEditProductAssetCategoryPeriodViewModelMapper : MapperBase<ProductAssetCategoryPeriodDto, EditProductAssetCategoryPeriodViewModel>
    {
        public override partial EditProductAssetCategoryPeriodViewModel Map(ProductAssetCategoryPeriodDto source);

        public override partial void Map(ProductAssetCategoryPeriodDto source, EditProductAssetCategoryPeriodViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateProductAssetCategoryPeriodViewModelToCreateProductAssetCategoryPeriodDtoMapper : MapperBase<CreateProductAssetCategoryPeriodViewModel, CreateProductAssetCategoryPeriodDto>
    {
        public override partial CreateProductAssetCategoryPeriodDto Map(CreateProductAssetCategoryPeriodViewModel source);

        public override partial void Map(CreateProductAssetCategoryPeriodViewModel source, CreateProductAssetCategoryPeriodDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class EditProductAssetCategoryPeriodViewModelToUpdateProductAssetCategoryPeriodDtoMapper : MapperBase<EditProductAssetCategoryPeriodViewModel, UpdateProductAssetCategoryPeriodDto>
    {
        public override partial UpdateProductAssetCategoryPeriodDto Map(EditProductAssetCategoryPeriodViewModel source);

        public override partial void Map(EditProductAssetCategoryPeriodViewModel source, UpdateProductAssetCategoryPeriodDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class GrantedStoreDtoToCreateEditGrantedStoreViewModelMapper : MapperBase<GrantedStoreDto, CreateEditGrantedStoreViewModel>
    {
        public override partial CreateEditGrantedStoreViewModel Map(GrantedStoreDto source);

        public override partial void Map(GrantedStoreDto source, CreateEditGrantedStoreViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateEditGrantedStoreViewModelToCreateUpdateGrantedStoreDtoMapper : MapperBase<CreateEditGrantedStoreViewModel, CreateUpdateGrantedStoreDto>
    {
        public override partial CreateUpdateGrantedStoreDto Map(CreateEditGrantedStoreViewModel source);

        public override partial void Map(CreateEditGrantedStoreViewModel source, CreateUpdateGrantedStoreDto destination);
    }
}
