using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    [DependsOn(
        typeof(EShopPaymentsWeChatPayApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EShopPaymentsWeChatPayHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "WeChatPay";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopPaymentsWeChatPayApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
