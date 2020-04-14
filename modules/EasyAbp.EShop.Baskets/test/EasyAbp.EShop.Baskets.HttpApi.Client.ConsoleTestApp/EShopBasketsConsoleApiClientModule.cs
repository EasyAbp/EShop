using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Baskets
{
    [DependsOn(
        typeof(EShopBasketsHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EShopBasketsConsoleApiClientModule : AbpModule
    {
        
    }
}
