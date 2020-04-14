using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Orders
{
    [DependsOn(
        typeof(EShopOrdersHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EShopOrdersConsoleApiClientModule : AbpModule
    {
        
    }
}
