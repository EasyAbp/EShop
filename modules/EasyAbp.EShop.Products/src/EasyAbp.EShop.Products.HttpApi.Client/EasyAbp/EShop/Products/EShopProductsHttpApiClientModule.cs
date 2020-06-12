using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(EShopProductsApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EShopProductsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "EShopProducts";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopProductsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
