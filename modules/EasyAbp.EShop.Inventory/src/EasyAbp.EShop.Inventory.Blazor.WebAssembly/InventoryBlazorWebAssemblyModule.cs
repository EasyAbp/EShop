using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Inventory.Blazor.WebAssembly
{
    [DependsOn(
        typeof(InventoryBlazorModule),
        typeof(InventoryHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class InventoryBlazorWebAssemblyModule : AbpModule
    {
        
    }
}