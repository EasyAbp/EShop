using AutoMapper;
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
using System.Linq;
using Volo.Abp.AutoMapper;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products
{
    public class ProductsApplicationAutoMapperProfile : Profile, ISingletonDependency
    {
        public ProductsApplicationAutoMapperProfile() 
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Product, ProductDto>()
                .Ignore(dto => dto.ProductGroupDisplayName)
                .Ignore(dto => dto.Sold)
                .Ignore(dto => dto.MinimumPrice)
                .Ignore(dto => dto.MaximumPrice);
            CreateMap<ProductDetail, ProductDetailDto>();
            CreateMap<ProductAttribute, ProductAttributeDto>();
            CreateMap<ProductAttributeOption, ProductAttributeOptionDto>();
            CreateMap<ProductSku, ProductSkuDto>()
                .Ignore(dto => dto.Price)
                .Ignore(dto => dto.DiscountedPrice)
                .Ignore(dto => dto.Inventory)
                .Ignore(dto => dto.Sold);
            CreateMap<CreateUpdateProductDetailDto, ProductDetail>(MemberList.Source)
                .ForSourceMember(dto => dto.StoreId, opt => opt.DoNotValidate());
            CreateMap<CreateUpdateProductAttributeDto, ProductAttribute>(MemberList.Source);
            CreateMap<CreateUpdateProductAttributeOptionDto, ProductAttributeOption>(MemberList.Source);
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategorySummaryDto>();
            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<ProductHistory, ProductHistoryDto>();
            CreateMap<ProductDetailHistory, ProductDetailHistoryDto>();
            CreateMap<ProductView, ProductViewDto>();
            CreateMap<Product, ProductView>(MemberList.Destination);
        }
    }
}
