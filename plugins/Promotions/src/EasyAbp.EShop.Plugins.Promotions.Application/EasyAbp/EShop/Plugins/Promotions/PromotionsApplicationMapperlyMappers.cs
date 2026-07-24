using EasyAbp.EShop.Plugins.Promotions.Promotions;
using EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Plugins.Promotions
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class PromotionToPromotionDtoMapper : MapperBase<Promotion, PromotionDto>
    {
        public override partial PromotionDto Map(Promotion source);

        public override partial void Map(Promotion source, PromotionDto destination);
    }
}
