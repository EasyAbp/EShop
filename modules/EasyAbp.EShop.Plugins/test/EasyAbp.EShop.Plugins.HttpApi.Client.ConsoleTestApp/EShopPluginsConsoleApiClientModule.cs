using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins
{
    [DependsOn(
        typeof(EShopPluginsHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EShopPluginsConsoleApiClientModule : AbpModule
    {
        
    }
}
