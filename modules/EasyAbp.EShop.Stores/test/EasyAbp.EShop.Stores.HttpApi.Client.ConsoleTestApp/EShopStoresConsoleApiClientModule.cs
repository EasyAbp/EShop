using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Stores
{
    [DependsOn(
        typeof(EShopStoresHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EShopStoresConsoleApiClientModule : AbpModule
    {
        
    }
}
