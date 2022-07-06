using AutoMapper;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

namespace EasyAbp.EShop.Plugins.FlashSales;

public class EShopPluginsFlashSalesDomainAutoMapperProfile : Profile
{
    public EShopPluginsFlashSalesDomainAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<FlashSalePlan, FlashSalePlanEto>()
            .MapExtraProperties();
    }
}
