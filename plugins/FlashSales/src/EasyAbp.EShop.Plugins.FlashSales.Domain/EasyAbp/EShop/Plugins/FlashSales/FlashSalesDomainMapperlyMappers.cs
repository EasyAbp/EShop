using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Plugins.FlashSales
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    [MapExtraProperties]
    public partial class FlashSalePlanToFlashSalePlanEtoMapper : MapperBase<FlashSalePlan, FlashSalePlanEto>
    {
        public override partial FlashSalePlanEto Map(FlashSalePlan source);

        public override partial void Map(FlashSalePlan source, FlashSalePlanEto destination);
    }
}
