using EasyAbp.EShop.Stores.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Stores
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(EShopStoresEntityFrameworkCoreTestModule)
        )]
    public class EShopStoresDomainTestModule : AbpModule
    {
        
    }
}
