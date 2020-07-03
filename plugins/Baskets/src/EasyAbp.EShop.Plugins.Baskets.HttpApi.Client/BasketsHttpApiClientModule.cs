using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [DependsOn(
        typeof(BasketsApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class BasketsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Baskets";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(BasketsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
