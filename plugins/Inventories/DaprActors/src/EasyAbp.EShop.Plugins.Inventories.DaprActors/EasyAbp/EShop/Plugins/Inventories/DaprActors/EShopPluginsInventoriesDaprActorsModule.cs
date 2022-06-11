using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Inventories.DaprActors;

[DependsOn(
    typeof(EShopPluginsInventoriesDaprActorsAbstractionsModule)
)]
public class EShopPluginsInventoriesDaprActorsModule : AbpModule
{
}