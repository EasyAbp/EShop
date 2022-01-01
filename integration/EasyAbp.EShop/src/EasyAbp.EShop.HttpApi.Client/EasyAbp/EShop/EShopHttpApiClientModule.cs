using EasyAbp.EShop.Plugins;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop
{
    [DependsOn(
        typeof(EShopApplicationContractsModule),
        typeof(AbpHttpClientModule),
        typeof(EShopOrdersHttpApiClientModule),
        typeof(EShopPaymentsHttpApiClientModule),
        typeof(EShopPluginsHttpApiClientModule),
        typeof(EShopProductsHttpApiClientModule),
        typeof(EShopStoresHttpApiClientModule)
    )]
    public class EShopHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = EShopRemoteServiceConsts.RemoteServiceName;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopApplicationContractsModule).Assembly,
                RemoteServiceName
            );
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopApplicationContractsModule>();
            });
        }
    }
}
