using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Inventory
{
    [DependsOn(
        typeof(InventoryHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class InventoryConsoleApiClientModule : AbpModule
    {
        
    }
}
