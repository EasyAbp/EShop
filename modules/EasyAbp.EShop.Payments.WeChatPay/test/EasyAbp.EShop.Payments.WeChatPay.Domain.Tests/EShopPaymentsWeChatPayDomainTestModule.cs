using EasyAbp.EShop.Payments.WeChatPay.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(EShopPaymentsWeChatPayEntityFrameworkCoreTestModule)
        )]
    public class EShopPaymentsWeChatPayDomainTestModule : AbpModule
    {
        
    }
}
