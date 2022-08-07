using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Plugins.Inventories.DaprActors;

[DependsOn(
    typeof(AbpTimingModule),
    typeof(EShopPluginsInventoriesDaprActorsAbstractionsModule)
)]
public class EShopPluginsInventoriesDaprActorsModule : AbpModule
{
}