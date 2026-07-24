using EasyAbp.EShop.Products.Categories;
using EasyAbp.EShop.Products.Categories.Dtos;
using EasyAbp.EShop.Products.ProductCategories;
using EasyAbp.EShop.Products.ProductCategories.Dtos;
using EasyAbp.EShop.Products.ProductDetailHistories;
using EasyAbp.EShop.Products.ProductDetailHistories.Dtos;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.ProductHistories;
using EasyAbp.EShop.Products.ProductHistories.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Products
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductToProductDtoMapper : MapperBase<Product, ProductDto>
    {
        [UseMapper] private readonly ProductSkuToProductSkuDtoMapper _productSkuMapper;
        [UseMapper] private readonly ProductAttributeToProductAttributeDtoMapper _productAttributeMapper;

        public ProductToProductDtoMapper(
            ProductSkuToProductSkuDtoMapper productSkuMapper,
            ProductAttributeToProductAttributeDtoMapper productAttributeMapper)
        {
            _productSkuMapper = productSkuMapper;
            _productAttributeMapper = productAttributeMapper;
        }

        [MapperIgnoreTarget(nameof(ProductDto.ProductGroupDisplayName))]
        [MapperIgnoreTarget(nameof(ProductDto.Sold))]
        [MapperIgnoreTarget(nameof(ProductDto.MinimumPrice))]
        [MapperIgnoreTarget(nameof(ProductDto.MaximumPrice))]
        public override partial ProductDto Map(Product source);

        [MapperIgnoreTarget(nameof(ProductDto.ProductGroupDisplayName))]
        [MapperIgnoreTarget(nameof(ProductDto.Sold))]
        [MapperIgnoreTarget(nameof(ProductDto.MinimumPrice))]
        [MapperIgnoreTarget(nameof(ProductDto.MaximumPrice))]
        public override partial void Map(Product source, ProductDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductDetailToProductDetailDtoMapper : MapperBase<ProductDetail, ProductDetailDto>
    {
        public override partial ProductDetailDto Map(ProductDetail source);

        public override partial void Map(ProductDetail source, ProductDetailDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductAttributeToProductAttributeDtoMapper : MapperBase<ProductAttribute, ProductAttributeDto>
    {
        [UseMapper] private readonly ProductAttributeOptionToProductAttributeOptionDtoMapper _optionMapper;

        public ProductAttributeToProductAttributeDtoMapper(
            ProductAttributeOptionToProductAttributeOptionDtoMapper optionMapper)
        {
            _optionMapper = optionMapper;
        }

        public override partial ProductAttributeDto Map(ProductAttribute source);

        public override partial void Map(ProductAttribute source, ProductAttributeDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductAttributeOptionToProductAttributeOptionDtoMapper : MapperBase<ProductAttributeOption, ProductAttributeOptionDto>
    {
        public override partial ProductAttributeOptionDto Map(ProductAttributeOption source);

        public override partial void Map(ProductAttributeOption source, ProductAttributeOptionDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductSkuToProductSkuDtoMapper : MapperBase<ProductSku, ProductSkuDto>
    {
        [MapperIgnoreTarget(nameof(ProductSkuDto.Price))]
        [MapperIgnoreTarget(nameof(ProductSkuDto.Inventory))]
        [MapperIgnoreTarget(nameof(ProductSkuDto.Sold))]
        public override partial ProductSkuDto Map(ProductSku source);

        [MapperIgnoreTarget(nameof(ProductSkuDto.Price))]
        [MapperIgnoreTarget(nameof(ProductSkuDto.Inventory))]
        [MapperIgnoreTarget(nameof(ProductSkuDto.Sold))]
        public override partial void Map(ProductSku source, ProductSkuDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CategoryToCategoryDtoMapper : MapperBase<Category, CategoryDto>
    {
        public override partial CategoryDto Map(Category source);

        public override partial void Map(Category source, CategoryDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CategoryToCategorySummaryDtoMapper : MapperBase<Category, CategorySummaryDto>
    {
        public override partial CategorySummaryDto Map(Category source);

        public override partial void Map(Category source, CategorySummaryDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductCategoryToProductCategoryDtoMapper : MapperBase<ProductCategory, ProductCategoryDto>
    {
        public override partial ProductCategoryDto Map(ProductCategory source);

        public override partial void Map(ProductCategory source, ProductCategoryDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductHistoryToProductHistoryDtoMapper : MapperBase<ProductHistory, ProductHistoryDto>
    {
        public override partial ProductHistoryDto Map(ProductHistory source);

        public override partial void Map(ProductHistory source, ProductHistoryDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductDetailHistoryToProductDetailHistoryDtoMapper : MapperBase<ProductDetailHistory, ProductDetailHistoryDto>
    {
        public override partial ProductDetailHistoryDto Map(ProductDetailHistory source);

        public override partial void Map(ProductDetailHistory source, ProductDetailHistoryDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductViewToProductViewDtoMapper : MapperBase<ProductView, ProductViewDto>
    {
        public override partial ProductViewDto Map(ProductView source);

        public override partial void Map(ProductView source, ProductViewDto destination);
    }
}
