using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payment
{
    [DependsOn(
        typeof(EShopPaymentApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EShopPaymentHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Payment";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopPaymentApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
