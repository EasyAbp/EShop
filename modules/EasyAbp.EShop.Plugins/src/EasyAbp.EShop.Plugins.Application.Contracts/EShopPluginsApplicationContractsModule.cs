using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Authorization;

namespace EasyAbp.EShop.Plugins
{
    [DependsOn(
        typeof(EShopPluginsDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class EShopPluginsApplicationContractsModule : AbpModule
    {

    }
}
