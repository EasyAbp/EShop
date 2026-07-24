using EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;
using EasyAbp.EShop.Plugins.Promotions.Web.Pages.EShop.Plugins.Promotions.Promotions.Promotion.ViewModels;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Plugins.Promotions.Web
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class PromotionDtoToEditPromotionViewModelMapper : MapperBase<PromotionDto, EditPromotionViewModel>
    {
        public override partial EditPromotionViewModel Map(PromotionDto source);

        public override partial void Map(PromotionDto source, EditPromotionViewModel destination);
    }
}
