using EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;
using AutoMapper;
using EasyAbp.EShop.Plugins.Baskets.Web.Pages.EShop.Plugins.Baskets.BasketItems.BasketItem.ViewModels;

namespace EasyAbp.EShop.Plugins.Baskets.Web
{
    public class BasketsWebAutoMapperProfile : Profile
    {
        public BasketsWebAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<BasketItemDto, EditBasketItemViewModel>();
            CreateMap<CreateBasketItemViewModel, CreateBasketItemDto>();
            CreateMap<EditBasketItemViewModel, UpdateBasketItemDto>();
        }
    }
}
