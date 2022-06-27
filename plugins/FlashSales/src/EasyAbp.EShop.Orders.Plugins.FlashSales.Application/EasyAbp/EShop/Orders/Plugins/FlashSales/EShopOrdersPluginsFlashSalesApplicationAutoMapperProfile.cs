using AutoMapper;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.AutoMapper;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Plugins.FlashSales
{
    public class EShopOrdersPluginsFlashSalesApplicationAutoMapperProfile : Profile
    {
        public EShopOrdersPluginsFlashSalesApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<FlashSalesProductEto, ProductDto>(MemberList.Destination)
                .Ignore(dto => dto.Sold)
                .Ignore(dto => dto.MinimumPrice)
                .Ignore(dto => dto.MaximumPrice)
                .MapExtraProperties();
            CreateMap<FlashSalesProductSkuEto, ProductSkuDto>(MemberList.Destination)
                .ForSourceMember(entity => entity.SerializedAttributeOptionIds, opt => opt.DoNotValidate())
                .Ignore(dto => dto.DiscountedPrice)
                .Ignore(dto => dto.Inventory)
                .Ignore(dto => dto.Sold)
                .MapExtraProperties();
            CreateMap<FlashSalesProductAttributeEto, ProductAttributeDto>(MemberList.Destination)
                .MapExtraProperties();
            CreateMap<FlashSalesProductAttributeOptionEto, ProductAttributeOptionDto>(MemberList.Destination)
                .MapExtraProperties();
            CreateMap<FlashSalesProductDetailEto, ProductDetailDto>(MemberList.Destination)
                .MapExtraProperties();
        }
    }
}
