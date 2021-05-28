using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Inventory.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(InventoryBlazorModule)
        )]
    public class InventoryBlazorServerModule : AbpModule
    {
        
    }
}