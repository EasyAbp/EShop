using AutoMapper;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales;

public class FlashSalesApplicationAutoMapperProfile : Profile
{
    public FlashSalesApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<FlashSalePlan, FlashSalePlanDto>()
            .MapExtraProperties();
        CreateMap<FlashSalePlanCreateDto, FlashSalePlan>(MemberList.Source);
        CreateMap<FlashSalePlanUpdateDto, FlashSalePlan>(MemberList.Source);
        CreateMap<FlashSalePlan, FlashSalePlanCacheItem>()
            .MapExtraProperties();
        CreateMap<FlashSalePlanCacheItem, FlashSalePlanEto>()
            .MapExtraProperties();

        CreateMap<FlashSaleResult, FlashSaleResultDto>()
            .MapExtraProperties();
    }
}
