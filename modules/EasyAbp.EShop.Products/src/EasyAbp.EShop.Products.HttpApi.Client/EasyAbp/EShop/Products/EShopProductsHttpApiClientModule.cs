using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(EShopProductsApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EShopProductsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = EShopProductsRemoteServiceConsts.RemoteServiceName;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopProductsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopProductsApplicationContractsModule>();
            });
        }
    }
}
