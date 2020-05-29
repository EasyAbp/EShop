using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop
{
    [DependsOn(
        typeof(EShopHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EShopConsoleApiClientModule : AbpModule
    {
        
    }
}
