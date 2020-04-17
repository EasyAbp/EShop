using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payment.WeChatPay
{
    [DependsOn(
        typeof(EShopPaymentWeChatPayApplicationModule),
        typeof(EShopPaymentWeChatPayDomainTestModule)
        )]
    public class EShopPaymentWeChatPayApplicationTestModule : AbpModule
    {

    }
}
