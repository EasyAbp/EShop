using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Mapperly;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Plugins.Coupons
{
    [DependsOn(
        typeof(EShopPluginsCouponsApplicationContractsModule),
        typeof(AbpHttpClientModule),
        typeof(AbpMapperlyModule)
    )]
    public class EShopPluginsCouponsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = EShopPluginsCouponsRemoteServiceConsts.RemoteServiceName;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMapperlyObjectMapper<EShopPluginsCouponsHttpApiClientModule>();
            
            context.Services.AddHttpClientProxies(
                typeof(EShopPluginsCouponsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopPluginsCouponsApplicationContractsModule>();
            });
        }
    }
}
