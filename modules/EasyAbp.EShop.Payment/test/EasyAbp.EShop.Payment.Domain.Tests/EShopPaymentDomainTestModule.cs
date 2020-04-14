using EasyAbp.EShop.Payment.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payment
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(EShopPaymentEntityFrameworkCoreTestModule)
        )]
    public class EShopPaymentDomainTestModule : AbpModule
    {
        
    }
}
