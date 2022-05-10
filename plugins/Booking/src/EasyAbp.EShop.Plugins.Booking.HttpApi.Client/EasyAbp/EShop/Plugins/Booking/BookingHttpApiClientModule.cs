using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Plugins.Booking;

[DependsOn(
    typeof(BookingApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class BookingHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(BookingApplicationContractsModule).Assembly,
            BookingRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BookingHttpApiClientModule>();
        });

    }
}
