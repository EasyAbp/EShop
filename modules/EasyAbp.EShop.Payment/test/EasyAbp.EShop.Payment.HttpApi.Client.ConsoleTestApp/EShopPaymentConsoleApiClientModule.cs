using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payment
{
    [DependsOn(
        typeof(EShopPaymentHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EShopPaymentConsoleApiClientModule : AbpModule
    {
        
    }
}
