using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Orders
{
    [DependsOn(
        typeof(EShopOrdersApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EShopOrdersHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Orders";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopOrdersApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
