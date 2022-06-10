using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Inventories.DaprActors;

[DependsOn(
    typeof(EShopPluginsInventoriesDaprActorsAbstractionsModule)
)]
public class EShopPluginsInventoriesDaprActorsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddActors(options => { options.Actors.RegisterActor<InventoryActor>(); });
    }
}