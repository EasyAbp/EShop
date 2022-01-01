using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Stores
{
    [DependsOn(
        typeof(EShopStoresApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EShopStoresHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = EShopStoresRemoteServiceConsts.RemoteServiceName;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopStoresApplicationContractsModule).Assembly,
                RemoteServiceName
            );
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopStoresApplicationContractsModule>();
            });
        }
    }
}
