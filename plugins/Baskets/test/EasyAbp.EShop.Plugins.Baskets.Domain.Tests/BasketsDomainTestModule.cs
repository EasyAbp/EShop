using EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Baskets
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(BasketsEntityFrameworkCoreTestModule)
        )]
    public class BasketsDomainTestModule : AbpModule
    {
        
    }
}
