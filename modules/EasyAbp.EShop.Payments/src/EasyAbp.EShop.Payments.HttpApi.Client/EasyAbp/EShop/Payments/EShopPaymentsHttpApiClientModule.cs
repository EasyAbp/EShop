using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments
{
    [DependsOn(
        typeof(EShopPaymentsApplicationContractsModule),
        typeof(AbpHttpClientModule)
    )]
    public class EShopPaymentsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Payments";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopPaymentsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
