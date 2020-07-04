using EasyAbp.EShop.Plugins.Baskets.BasketItems;
using EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;
using AutoMapper;

namespace EasyAbp.EShop.Plugins.Baskets
{
    public class BasketsApplicationAutoMapperProfile : Profile
    {
        public BasketsApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<BasketItem, BasketItemDto>();
        }
    }
}
