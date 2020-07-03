using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [DependsOn(
        typeof(BasketsHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class BasketsConsoleApiClientModule : AbpModule
    {
        
    }
}
