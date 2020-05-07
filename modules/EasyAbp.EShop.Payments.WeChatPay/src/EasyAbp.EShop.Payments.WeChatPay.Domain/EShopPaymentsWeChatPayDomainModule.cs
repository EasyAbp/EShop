using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    [DependsOn(
        typeof(EShopPaymentsWeChatPayDomainSharedModule)
        )]
    public class EShopPaymentsWeChatPayDomainModule : AbpModule
    {

    }
}
