using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins
{
    [DependsOn(
        typeof(EShopPluginsDomainSharedModule)
        )]
    public class EShopPluginsDomainModule : AbpModule
    {

    }
}
