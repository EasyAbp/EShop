using EasyAbp.EShop.Plugins.Baskets.BasketItems;
using EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class BasketItemToBasketItemDtoMapper : MapperBase<BasketItem, BasketItemDto>
    {
        public override partial BasketItemDto Map(BasketItem source);

        public override partial void Map(BasketItem source, BasketItemDto destination);
    }
}
