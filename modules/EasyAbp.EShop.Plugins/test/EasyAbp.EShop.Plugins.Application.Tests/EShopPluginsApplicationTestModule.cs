using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins
{
    [DependsOn(
        typeof(EShopPluginsApplicationModule),
        typeof(EShopPluginsDomainTestModule)
        )]
    public class EShopPluginsApplicationTestModule : AbpModule
    {

    }
}
