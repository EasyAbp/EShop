using EasyAbp.EShop.Orders.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Orders
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(EShopOrdersEntityFrameworkCoreTestModule)
        )]
    public class EShopOrdersDomainTestModule : AbpModule
    {
        
    }
}
