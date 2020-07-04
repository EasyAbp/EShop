using AutoMapper;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Products
{
    public class ProductsDomainAutoMapperProfile : Profile
    {
        public ProductsDomainAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Product, ProductEto>();
            CreateMap<ProductAttribute, ProductAttributeEto>();
            CreateMap<ProductAttributeOption, ProductAttributeOptionEto>();
            CreateMap<ProductSku, ProductSkuEto>();
        }
    }
}
