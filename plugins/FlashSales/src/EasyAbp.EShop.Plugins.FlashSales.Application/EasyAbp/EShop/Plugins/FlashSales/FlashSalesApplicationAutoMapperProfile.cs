using AutoMapper;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales;

public class FlashSalesApplicationAutoMapperProfile : Profile
{
    public FlashSalesApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<FlashSalePlan, FlashSalePlanDto>()
            .MapExtraProperties()
            .ForSourceMember(x => x.ExtraProperties,
                x => x.DoNotValidate()); // todo: https://github.com/abpframework/abp/issues/15404
        CreateMap<FlashSalePlanCreateDto, FlashSalePlan>(MemberList.Source);
        CreateMap<FlashSalePlanUpdateDto, FlashSalePlan>(MemberList.Source);
        CreateMap<FlashSalePlan, FlashSalePlanCacheItem>()
            .MapExtraProperties()
            .ForSourceMember(x => x.ExtraProperties,
                x => x.DoNotValidate()); // todo: https://github.com/abpframework/abp/issues/15404
        CreateMap<FlashSalePlanCacheItem, FlashSalePlanEto>()
            .MapExtraProperties()
            .ForSourceMember(x => x.ExtraProperties,
                x => x.DoNotValidate()); // todo: https://github.com/abpframework/abp/issues/15404
        CreateMap<FlashSaleResult, FlashSaleResultDto>()
            .MapExtraProperties()
            .ForSourceMember(x => x.ExtraProperties,
                x => x.DoNotValidate()); // todo: https://github.com/abpframework/abp/issues/15404
        CreateMap<ProductDto, ProductCacheItem>(MemberList.Source)
            .MapExtraProperties()
            .ForSourceMember(x => x.ExtraProperties,
                x => x.DoNotValidate()); // todo: https://github.com/abpframework/abp/issues/15404
    }
}