using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payment.WeChatPay
{
    [DependsOn(
        typeof(EShopPaymentWeChatPayHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EShopPaymentWeChatPayConsoleApiClientModule : AbpModule
    {
        
    }
}
