using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payment.WeChatPay
{
    [DependsOn(
        typeof(EShopPaymentWeChatPayDomainSharedModule)
        )]
    public class EShopPaymentWeChatPayDomainModule : AbpModule
    {

    }
}
