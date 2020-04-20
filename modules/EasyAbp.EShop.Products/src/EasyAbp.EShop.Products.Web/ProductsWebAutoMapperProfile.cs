using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.Categories.Dtos;
using EasyAbp.EShop.Products.ProductTypes.Dtos;
using AutoMapper;
using Volo.Abp.AutoMapper;

namespace EasyAbp.EShop.Products.Web
{
    public class ProductsWebAutoMapperProfile : Profile
    {
        public ProductsWebAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<ProductDto, CreateUpdateProductDto>();
            CreateMap<ProductDetailDto, CreateUpdateProductDetailDto>();
            CreateMap<CategoryDto, CreateUpdateCategoryDto>();
            CreateMap<ProductTypeDto, CreateUpdateProductTypeDto>();
        }
    }
}
