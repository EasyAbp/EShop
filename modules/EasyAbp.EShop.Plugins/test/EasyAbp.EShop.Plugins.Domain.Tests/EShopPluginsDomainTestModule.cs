using EasyAbp.EShop.Plugins.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(EShopPluginsEntityFrameworkCoreTestModule)
        )]
    public class EShopPluginsDomainTestModule : AbpModule
    {
        
    }
}
