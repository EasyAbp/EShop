using AutoMapper;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales;

public class FlashSalesApplicationAutoMapperProfile : Profile
{
    public FlashSalesApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<FlashSalesPlan, FlashSalesPlanDto>()
            .MapExtraProperties();
        CreateMap<FlashSalesPlanCreateDto, FlashSalesPlan>(MemberList.Source);
        CreateMap<FlashSalesPlanUpdateDto, FlashSalesPlan>(MemberList.Source);
        CreateMap<FlashSalesPlan, FlashSalesPlanCacheItem>()
            .MapExtraProperties();
        CreateMap<FlashSalesPlanCacheItem, FlashSalesPlanEto>()
            .MapExtraProperties();

        CreateMap<FlashSalesResult, FlashSalesResultDto>()
            .MapExtraProperties();
    }
}
