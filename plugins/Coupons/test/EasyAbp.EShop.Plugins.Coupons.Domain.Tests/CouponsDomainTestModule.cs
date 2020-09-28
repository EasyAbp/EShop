using EasyAbp.EShop.Plugins.Coupons.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Coupons
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(CouponsEntityFrameworkCoreTestModule)
        )]
    public class CouponsDomainTestModule : AbpModule
    {
        
    }
}
