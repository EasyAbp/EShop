using EasyAbp.EShop.Products;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Inventories.DaprActors;

[DependsOn(
    typeof(AbpAspNetCoreModule),
    typeof(EShopPluginsInventoriesDaprActorsModule)
)]
public class EShopPluginsInventoriesDaprActorsAspNetCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddActors(options => { options.Actors.RegisterActor<InventoryActor>(); });
        
        Configure<AbpEndpointRouterOptions>(options =>
        {
            options.EndpointConfigureActions.Add(ctx =>
            {
                // Register actors handlers that interface with the Dapr runtime.
                ctx.Endpoints.MapActorsHandlers();
            });
        });
    }
}