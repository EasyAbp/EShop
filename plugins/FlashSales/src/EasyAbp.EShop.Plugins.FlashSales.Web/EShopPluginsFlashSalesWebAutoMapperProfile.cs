using AutoMapper;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalesPlans.FlashSalesPlan.ViewModels;
using EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalesResults.FlashSalesResult.ViewModels;

namespace EasyAbp.EShop.Plugins.FlashSales.Web;

public class EShopPluginsFlashSalesWebAutoMapperProfile : Profile
{
    public EShopPluginsFlashSalesWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<CreateFlashSalesPlanViewModel, FlashSalesPlanCreateDto>()
            .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore());
        CreateMap<EditFlashSalesPlanViewModel, FlashSalesPlanUpdateDto>()
            .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore());
        CreateMap<FlashSalesPlanDto, EditFlashSalesPlanViewModel>()
            .ForSourceMember(dest => dest.ExtraProperties, opt => opt.DoNotValidate());
        CreateMap<FlashSalesResultDto, ViewFlashSalesResultViewModel>()
            .ForSourceMember(dest => dest.ExtraProperties, opt => opt.DoNotValidate());
    }
}
