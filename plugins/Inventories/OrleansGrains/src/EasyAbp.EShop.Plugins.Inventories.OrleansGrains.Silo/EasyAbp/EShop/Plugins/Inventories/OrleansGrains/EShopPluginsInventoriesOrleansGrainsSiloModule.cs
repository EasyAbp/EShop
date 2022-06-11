using EasyAbp.EShop.Products;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Inventories.OrleansGrains;

[DependsOn(
    typeof(EShopPluginsInventoriesOrleansGrainsModule)
)]
public class EShopPluginsInventoriesOrleansGrainsSiloModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}