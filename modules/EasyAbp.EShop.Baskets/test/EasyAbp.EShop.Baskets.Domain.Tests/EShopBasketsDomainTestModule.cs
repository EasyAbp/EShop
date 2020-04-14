using EasyAbp.EShop.Baskets.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Baskets
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(EShopBasketsEntityFrameworkCoreTestModule)
        )]
    public class EShopBasketsDomainTestModule : AbpModule
    {
        
    }
}
