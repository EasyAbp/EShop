using EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;
using AutoMapper;
using EasyAbp.EShop.Plugins.Promotions.Web.Pages.EShop.Plugins.Promotions.Promotions.Promotion.ViewModels;

namespace EasyAbp.EShop.Plugins.Promotions.Web;

public class PromotionsWebAutoMapperProfile : Profile
{
    public PromotionsWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<PromotionDto, EditPromotionViewModel>();
    }
}
