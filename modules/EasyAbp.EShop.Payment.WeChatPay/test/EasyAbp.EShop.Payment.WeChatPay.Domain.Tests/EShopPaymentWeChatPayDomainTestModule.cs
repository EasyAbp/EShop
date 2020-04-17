using EasyAbp.EShop.Payment.WeChatPay.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payment.WeChatPay
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(EShopPaymentWeChatPayEntityFrameworkCoreTestModule)
        )]
    public class EShopPaymentWeChatPayDomainTestModule : AbpModule
    {
        
    }
}
