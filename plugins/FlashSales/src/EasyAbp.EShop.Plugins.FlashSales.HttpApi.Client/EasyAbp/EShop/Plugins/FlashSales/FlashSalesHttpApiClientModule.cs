using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(FlashSalesApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class FlashSalesHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(FlashSalesApplicationContractsModule).Assembly,
            FlashSalesRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<FlashSalesHttpApiClientModule>();
        });

    }
}
