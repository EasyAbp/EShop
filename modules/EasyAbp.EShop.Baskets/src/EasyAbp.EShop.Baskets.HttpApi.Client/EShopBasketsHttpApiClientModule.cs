using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Baskets
{
    [DependsOn(
        typeof(EShopBasketsApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EShopBasketsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Baskets";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopBasketsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
