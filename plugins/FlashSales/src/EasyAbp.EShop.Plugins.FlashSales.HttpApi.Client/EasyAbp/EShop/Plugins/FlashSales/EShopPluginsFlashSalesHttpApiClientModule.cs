using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(EShopPluginsFlashSalesApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class EShopPluginsFlashSalesHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(EShopPluginsFlashSalesApplicationContractsModule).Assembly,
            EShopPluginsFlashSalesRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EShopPluginsFlashSalesHttpApiClientModule>();
        });

    }
}
