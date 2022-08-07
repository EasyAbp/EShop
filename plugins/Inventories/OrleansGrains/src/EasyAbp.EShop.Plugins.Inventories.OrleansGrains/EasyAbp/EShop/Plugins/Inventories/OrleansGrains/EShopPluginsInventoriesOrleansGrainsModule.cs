using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Plugins.Inventories.OrleansGrains;

[DependsOn(
    typeof(AbpTimingModule),
    typeof(EShopPluginsInventoriesOrleansGrainsAbstractionsModule)
)]
public class EShopPluginsInventoriesOrleansGrainsModule : AbpModule
{
}