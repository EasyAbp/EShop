using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Stores
{
    [DependsOn(
        typeof(EShopStoresApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EShopStoresHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Stores";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopStoresApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
