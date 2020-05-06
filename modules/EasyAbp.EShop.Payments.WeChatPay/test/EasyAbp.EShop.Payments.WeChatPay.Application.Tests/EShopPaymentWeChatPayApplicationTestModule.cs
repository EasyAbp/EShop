using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    [DependsOn(
        typeof(EShopPaymentsWeChatPayApplicationModule),
        typeof(EShopPaymentsWeChatPayDomainTestModule)
        )]
    public class EShopPaymentsWeChatPayApplicationTestModule : AbpModule
    {

    }
}
