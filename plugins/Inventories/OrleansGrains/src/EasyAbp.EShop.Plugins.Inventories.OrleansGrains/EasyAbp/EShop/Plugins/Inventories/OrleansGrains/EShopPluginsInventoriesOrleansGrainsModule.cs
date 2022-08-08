using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Inventories.OrleansGrains;

[DependsOn(
    typeof(EShopPluginsInventoriesOrleansGrainsAbstractionsModule)
)]
public class EShopPluginsInventoriesOrleansGrainsModule : AbpModule
{
}