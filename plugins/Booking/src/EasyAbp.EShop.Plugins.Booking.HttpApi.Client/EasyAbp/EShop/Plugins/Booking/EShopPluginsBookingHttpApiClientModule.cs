using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Plugins.Booking;

[DependsOn(
    typeof(EShopPluginsBookingApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class EShopPluginsBookingHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(EShopPluginsBookingApplicationContractsModule).Assembly,
            BookingRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EShopPluginsBookingHttpApiClientModule>();
        });

    }
}
