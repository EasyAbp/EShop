using EasyAbp.EShop.Plugins.Promotions.Promotions;
using EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;
using AutoMapper;

namespace EasyAbp.EShop.Plugins.Promotions;

public class PromotionsApplicationAutoMapperProfile : Profile
{
    public PromotionsApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Promotion, PromotionDto>();
    }
}
