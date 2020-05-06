using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    [DependsOn(
        typeof(EShopPaymentsWeChatPayHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EShopPaymentsWeChatPayConsoleApiClientModule : AbpModule
    {
        
    }
}
