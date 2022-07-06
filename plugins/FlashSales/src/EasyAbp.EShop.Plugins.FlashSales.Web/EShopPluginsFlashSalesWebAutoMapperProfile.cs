using AutoMapper;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalePlans.FlashSalePlan.ViewModels;
using EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSaleResults.FlashSaleResult.ViewModels;

namespace EasyAbp.EShop.Plugins.FlashSales.Web;

public class EShopPluginsFlashSalesWebAutoMapperProfile : Profile
{
    public EShopPluginsFlashSalesWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<CreateFlashSalePlanViewModel, FlashSalePlanCreateDto>(MemberList.Destination)
            .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore());
        CreateMap<EditFlashSalePlanViewModel, FlashSalePlanUpdateDto>(MemberList.Destination)
            .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore());
        CreateMap<FlashSalePlanDto, EditFlashSalePlanViewModel>(MemberList.Destination)
            .ForSourceMember(dest => dest.ExtraProperties, opt => opt.DoNotValidate());
        CreateMap<FlashSaleResultDto, ViewFlashSaleResultViewModel>(MemberList.Destination)
            .ForSourceMember(dest => dest.ExtraProperties, opt => opt.DoNotValidate());
    }
}
