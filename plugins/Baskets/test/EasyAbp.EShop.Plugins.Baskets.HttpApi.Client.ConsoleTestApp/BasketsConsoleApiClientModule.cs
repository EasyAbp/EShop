using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [DependsOn(
        typeof(EShopPluginsBasketsHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class BasketsConsoleApiClientModule : AbpModule
    {
        
    }
}
