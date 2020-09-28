using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Coupons
{
    [DependsOn(
        typeof(EShopPluginsCouponsHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class CouponsConsoleApiClientModule : AbpModule
    {
        
    }
}
