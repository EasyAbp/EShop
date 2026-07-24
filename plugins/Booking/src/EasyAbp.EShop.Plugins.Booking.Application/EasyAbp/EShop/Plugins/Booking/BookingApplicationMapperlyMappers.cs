using System.Collections.Generic;
using EasyAbp.EShop.Plugins.Booking.GrantedStores;
using EasyAbp.EShop.Plugins.Booking.GrantedStores.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Plugins.Booking
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductAssetToProductAssetDtoMapper : MapperBase<ProductAsset, ProductAssetDto>
    {
        public override partial ProductAssetDto Map(ProductAsset source);

        public override partial void Map(ProductAsset source, ProductAssetDto destination);

        // Mirror AutoMapper's default of mapping a null source collection to an empty collection.
        public override void AfterMap(ProductAsset source, ProductAssetDto destination)
        {
            destination.Periods ??= new List<ProductAssetPeriodDto>();
        }
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductAssetPeriodToProductAssetPeriodDtoMapper : MapperBase<ProductAssetPeriod, ProductAssetPeriodDto>
    {
        public override partial ProductAssetPeriodDto Map(ProductAssetPeriod source);

        public override partial void Map(ProductAssetPeriod source, ProductAssetPeriodDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductAssetCategoryToProductAssetCategoryDtoMapper : MapperBase<ProductAssetCategory, ProductAssetCategoryDto>
    {
        public override partial ProductAssetCategoryDto Map(ProductAssetCategory source);

        public override partial void Map(ProductAssetCategory source, ProductAssetCategoryDto destination);

        // Mirror AutoMapper's default of mapping a null source collection to an empty collection.
        public override void AfterMap(ProductAssetCategory source, ProductAssetCategoryDto destination)
        {
            destination.Periods ??= new List<ProductAssetCategoryPeriodDto>();
        }
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductAssetCategoryPeriodToProductAssetCategoryPeriodDtoMapper : MapperBase<ProductAssetCategoryPeriod, ProductAssetCategoryPeriodDto>
    {
        public override partial ProductAssetCategoryPeriodDto Map(ProductAssetCategoryPeriod source);

        public override partial void Map(ProductAssetCategoryPeriod source, ProductAssetCategoryPeriodDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class GrantedStoreToGrantedStoreDtoMapper : MapperBase<GrantedStore, GrantedStoreDto>
    {
        public override partial GrantedStoreDto Map(GrantedStore source);

        public override partial void Map(GrantedStore source, GrantedStoreDto destination);
    }
}
