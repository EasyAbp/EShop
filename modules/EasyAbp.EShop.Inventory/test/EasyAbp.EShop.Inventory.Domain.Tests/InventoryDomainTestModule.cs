using EasyAbp.EShop.Inventory.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Inventory
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(InventoryEntityFrameworkCoreTestModule)
        )]
    public class InventoryDomainTestModule : AbpModule
    {
        
    }
}
