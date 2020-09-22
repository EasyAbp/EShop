using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Coupons
{
    [DependsOn(
        typeof(EShopPluginsCouponsApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EShopPluginsCouponsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "EasyAbpEShopPluginsCoupons";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopPluginsCouponsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
