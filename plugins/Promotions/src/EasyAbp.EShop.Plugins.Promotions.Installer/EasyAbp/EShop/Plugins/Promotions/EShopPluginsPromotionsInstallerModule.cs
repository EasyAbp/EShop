using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Plugins.Promotions;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class EShopPluginsPromotionsInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EShopPluginsPromotionsInstallerModule>();
        });
    }
}
