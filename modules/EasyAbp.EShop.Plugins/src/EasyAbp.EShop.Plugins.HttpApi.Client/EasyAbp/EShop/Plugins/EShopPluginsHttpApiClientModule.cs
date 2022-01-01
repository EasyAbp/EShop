using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Plugins
{
    [DependsOn(
        typeof(EShopPluginsApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EShopPluginsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = EShopPluginsRemoteServiceConsts.RemoteServiceName;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopPluginsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopPluginsApplicationContractsModule>();
            });
        }
    }
}
