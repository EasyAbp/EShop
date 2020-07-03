using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [DependsOn(
        typeof(EShopPluginsBasketsApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EShopPluginsBasketsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "EShopPluginsBaskets";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopPluginsBasketsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
