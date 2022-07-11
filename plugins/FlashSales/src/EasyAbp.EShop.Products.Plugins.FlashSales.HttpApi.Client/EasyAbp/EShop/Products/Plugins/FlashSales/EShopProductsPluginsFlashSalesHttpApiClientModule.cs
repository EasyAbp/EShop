using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Products.Plugins.FlashSales;

[DependsOn(
    typeof(EShopProductsPluginsFlashSalesApplicationContractsModule)
)]
public class EShopProductsPluginsFlashSalesHttpApiClientModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
                typeof(EShopProductsApplicationContractsModule).Assembly,
                EShopProductsRemoteServiceConsts.RemoteServiceName
            );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EShopProductsApplicationContractsModule>();
        });
    }
}
