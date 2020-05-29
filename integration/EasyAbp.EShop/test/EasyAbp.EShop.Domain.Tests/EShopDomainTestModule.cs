using EasyAbp.EShop.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(EShopEntityFrameworkCoreTestModule)
        )]
    public class EShopDomainTestModule : AbpModule
    {
        
    }
}
