using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Plugins.FlashSales
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    [MapExtraProperties]
    public partial class FlashSalePlanToFlashSalePlanDtoMapper : MapperBase<FlashSalePlan, FlashSalePlanDto>
    {
        public override partial FlashSalePlanDto Map(FlashSalePlan source);

        public override partial void Map(FlashSalePlan source, FlashSalePlanDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    [MapExtraProperties]
    public partial class FlashSalePlanToFlashSalePlanCacheItemMapper : MapperBase<FlashSalePlan, FlashSalePlanCacheItem>
    {
        public override partial FlashSalePlanCacheItem Map(FlashSalePlan source);

        public override partial void Map(FlashSalePlan source, FlashSalePlanCacheItem destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    [MapExtraProperties]
    public partial class FlashSalePlanCacheItemToFlashSalePlanEtoMapper : MapperBase<FlashSalePlanCacheItem, FlashSalePlanEto>
    {
        public override partial FlashSalePlanEto Map(FlashSalePlanCacheItem source);

        public override partial void Map(FlashSalePlanCacheItem source, FlashSalePlanEto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    [MapExtraProperties]
    public partial class FlashSaleResultToFlashSaleResultDtoMapper : MapperBase<FlashSaleResult, FlashSaleResultDto>
    {
        public override partial FlashSaleResultDto Map(FlashSaleResult source);

        public override partial void Map(FlashSaleResult source, FlashSaleResultDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    [MapExtraProperties]
    public partial class ProductDtoToProductCacheItemMapper : MapperBase<ProductDto, ProductCacheItem>
    {
        public override partial ProductCacheItem Map(ProductDto source);

        public override partial void Map(ProductDto source, ProductCacheItem destination);
    }
}
