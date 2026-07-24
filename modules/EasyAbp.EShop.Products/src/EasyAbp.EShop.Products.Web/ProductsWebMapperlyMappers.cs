using System;
using System.Collections.Generic;
using System.Linq;
using EasyAbp.EShop.Products.Categories.Dtos;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Categories.Category.ViewModels;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product.ViewModels;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku.ViewModels;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Products.Web
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductDtoToCreateEditProductViewModelMapper : MapperBase<ProductDto, CreateEditProductViewModel>
    {
        [MapperIgnoreTarget(nameof(CreateEditProductViewModel.CategoryIds))]
        [MapperIgnoreTarget(nameof(CreateEditProductViewModel.ProductDetail))]
        [MapperIgnoreTarget(nameof(CreateEditProductViewModel.ProductAttributeNames))]
        [MapperIgnoreTarget(nameof(CreateEditProductViewModel.ProductAttributeOptionNames))]
        public override partial CreateEditProductViewModel Map(ProductDto source);

        [MapperIgnoreTarget(nameof(CreateEditProductViewModel.CategoryIds))]
        [MapperIgnoreTarget(nameof(CreateEditProductViewModel.ProductDetail))]
        [MapperIgnoreTarget(nameof(CreateEditProductViewModel.ProductAttributeNames))]
        [MapperIgnoreTarget(nameof(CreateEditProductViewModel.ProductAttributeOptionNames))]
        public override partial void Map(ProductDto source, CreateEditProductViewModel destination);

        public override void AfterMap(ProductDto source, CreateEditProductViewModel destination)
        {
            destination.ProductAttributeNames =
                source.ProductAttributes.Select(x => x.DisplayName).JoinAsString(",");

            destination.ProductAttributeOptionNames = source.ProductAttributes
                .Select(a => a.ProductAttributeOptions.Select(o => o.DisplayName).JoinAsString(","))
                .JoinAsString(Environment.NewLine);
        }
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateEditProductViewModelToCreateUpdateProductDtoMapper : MapperBase<CreateEditProductViewModel, CreateUpdateProductDto>
    {
        [MapperIgnoreTarget(nameof(CreateUpdateProductDto.ExtraProperties))]
        [MapperIgnoreTarget(nameof(CreateUpdateProductDto.ProductDetailId))]
        [MapperIgnoreTarget(nameof(CreateUpdateProductDto.ProductAttributes))]
        public override partial CreateUpdateProductDto Map(CreateEditProductViewModel source);

        [MapperIgnoreTarget(nameof(CreateUpdateProductDto.ExtraProperties))]
        [MapperIgnoreTarget(nameof(CreateUpdateProductDto.ProductDetailId))]
        [MapperIgnoreTarget(nameof(CreateUpdateProductDto.ProductAttributes))]
        public override partial void Map(CreateEditProductViewModel source, CreateUpdateProductDto destination);

        public override void AfterMap(CreateEditProductViewModel source, CreateUpdateProductDto destination)
        {
            destination.ProductAttributes = source.ProductAttributeNames
                .Split(",", StringSplitOptions.RemoveEmptyEntries).Select((s, i) =>
                    new CreateUpdateProductAttributeDto
                    {
                        DisplayName = s,
                        ProductAttributeOptions = new List<CreateUpdateProductAttributeOptionDto>(
                            source.ProductAttributeOptionNames.SplitToLines(StringSplitOptions.RemoveEmptyEntries)[i]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries).Select(o =>
                                    new CreateUpdateProductAttributeOptionDto { DisplayName = o.RemovePostFix("\r") }))
                    }).ToList();
        }
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductDetailDtoToCreateEditProductDetailViewModelMapper : MapperBase<ProductDetailDto, CreateEditProductDetailViewModel>
    {
        public override partial CreateEditProductDetailViewModel Map(ProductDetailDto source);

        public override partial void Map(ProductDetailDto source, CreateEditProductDetailViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateEditProductDetailViewModelToCreateUpdateProductDetailDtoMapper : MapperBase<CreateEditProductDetailViewModel, CreateUpdateProductDetailDto>
    {
        [MapperIgnoreTarget(nameof(CreateUpdateProductDetailDto.ExtraProperties))]
        public override partial CreateUpdateProductDetailDto Map(CreateEditProductDetailViewModel source);

        [MapperIgnoreTarget(nameof(CreateUpdateProductDetailDto.ExtraProperties))]
        public override partial void Map(CreateEditProductDetailViewModel source, CreateUpdateProductDetailDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateEditSkuProductDetailViewModelToCreateUpdateProductDetailDtoMapper : MapperBase<CreateEditSkuProductDetailViewModel, CreateUpdateProductDetailDto>
    {
        [MapperIgnoreTarget(nameof(CreateUpdateProductDetailDto.ExtraProperties))]
        public override partial CreateUpdateProductDetailDto Map(CreateEditSkuProductDetailViewModel source);

        [MapperIgnoreTarget(nameof(CreateUpdateProductDetailDto.ExtraProperties))]
        public override partial void Map(CreateEditSkuProductDetailViewModel source, CreateUpdateProductDetailDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductAttributeDtoToCreateEditProductAttributeViewModelMapper : MapperBase<ProductAttributeDto, CreateEditProductAttributeViewModel>
    {
        public override partial CreateEditProductAttributeViewModel Map(ProductAttributeDto source);

        public override partial void Map(ProductAttributeDto source, CreateEditProductAttributeViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateEditProductAttributeViewModelToCreateUpdateProductAttributeDtoMapper : MapperBase<CreateEditProductAttributeViewModel, CreateUpdateProductAttributeDto>
    {
        [MapperIgnoreTarget(nameof(CreateUpdateProductAttributeDto.ExtraProperties))]
        public override partial CreateUpdateProductAttributeDto Map(CreateEditProductAttributeViewModel source);

        [MapperIgnoreTarget(nameof(CreateUpdateProductAttributeDto.ExtraProperties))]
        public override partial void Map(CreateEditProductAttributeViewModel source, CreateUpdateProductAttributeDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateProductSkuViewModelToCreateProductSkuDtoMapper : MapperBase<CreateProductSkuViewModel, CreateProductSkuDto>
    {
        [MapperIgnoreTarget(nameof(CreateProductSkuDto.ExtraProperties))]
        [MapperIgnoreTarget(nameof(CreateProductSkuDto.ProductDetailId))]
        [MapperIgnoreTarget(nameof(CreateProductSkuDto.AttributeOptionIds))]
        public override partial CreateProductSkuDto Map(CreateProductSkuViewModel source);

        [MapperIgnoreTarget(nameof(CreateProductSkuDto.ExtraProperties))]
        [MapperIgnoreTarget(nameof(CreateProductSkuDto.ProductDetailId))]
        [MapperIgnoreTarget(nameof(CreateProductSkuDto.AttributeOptionIds))]
        public override partial void Map(CreateProductSkuViewModel source, CreateProductSkuDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class EditProductSkuViewModelToUpdateProductSkuDtoMapper : MapperBase<EditProductSkuViewModel, UpdateProductSkuDto>
    {
        [MapperIgnoreTarget(nameof(UpdateProductSkuDto.ProductDetailId))]
        [MapperIgnoreTarget(nameof(UpdateProductSkuDto.ExtraProperties))]
        public override partial UpdateProductSkuDto Map(EditProductSkuViewModel source);

        [MapperIgnoreTarget(nameof(UpdateProductSkuDto.ProductDetailId))]
        [MapperIgnoreTarget(nameof(UpdateProductSkuDto.ExtraProperties))]
        public override partial void Map(EditProductSkuViewModel source, UpdateProductSkuDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductSkuDtoToEditProductSkuViewModelMapper : MapperBase<ProductSkuDto, EditProductSkuViewModel>
    {
        [MapperIgnoreTarget(nameof(EditProductSkuViewModel.ProductDetail))]
        public override partial EditProductSkuViewModel Map(ProductSkuDto source);

        [MapperIgnoreTarget(nameof(EditProductSkuViewModel.ProductDetail))]
        public override partial void Map(ProductSkuDto source, EditProductSkuViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductAttributeOptionDtoToCreateEditProductAttributeOptionViewModelMapper : MapperBase<ProductAttributeOptionDto, CreateEditProductAttributeOptionViewModel>
    {
        public override partial CreateEditProductAttributeOptionViewModel Map(ProductAttributeOptionDto source);

        public override partial void Map(ProductAttributeOptionDto source, CreateEditProductAttributeOptionViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateEditProductAttributeOptionViewModelToCreateUpdateProductAttributeOptionDtoMapper : MapperBase<CreateEditProductAttributeOptionViewModel, CreateUpdateProductAttributeOptionDto>
    {
        [MapperIgnoreTarget(nameof(CreateUpdateProductAttributeOptionDto.ExtraProperties))]
        public override partial CreateUpdateProductAttributeOptionDto Map(CreateEditProductAttributeOptionViewModel source);

        [MapperIgnoreTarget(nameof(CreateUpdateProductAttributeOptionDto.ExtraProperties))]
        public override partial void Map(CreateEditProductAttributeOptionViewModel source, CreateUpdateProductAttributeOptionDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CategoryDtoToCreateEditCategoryViewModelMapper : MapperBase<CategoryDto, CreateEditCategoryViewModel>
    {
        public override partial CreateEditCategoryViewModel Map(CategoryDto source);

        public override partial void Map(CategoryDto source, CreateEditCategoryViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateEditCategoryViewModelToCreateUpdateCategoryDtoMapper : MapperBase<CreateEditCategoryViewModel, CreateUpdateCategoryDto>
    {
        [MapperIgnoreTarget(nameof(CreateUpdateCategoryDto.ExtraProperties))]
        public override partial CreateUpdateCategoryDto Map(CreateEditCategoryViewModel source);

        [MapperIgnoreTarget(nameof(CreateUpdateCategoryDto.ExtraProperties))]
        public override partial void Map(CreateEditCategoryViewModel source, CreateUpdateCategoryDto destination);
    }
}
