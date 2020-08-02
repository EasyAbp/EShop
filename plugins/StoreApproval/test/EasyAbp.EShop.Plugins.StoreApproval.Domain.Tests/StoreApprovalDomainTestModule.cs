using EasyAbp.EShop.Plugins.StoreApproval.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.StoreApproval
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(StoreApprovalEntityFrameworkCoreTestModule)
        )]
    public class StoreApprovalDomainTestModule : AbpModule
    {
        
    }
}
