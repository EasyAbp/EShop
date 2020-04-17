using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payment.WeChatPay
{
    [DependsOn(
        typeof(EShopPaymentWeChatPayApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EShopPaymentWeChatPayHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "WeChatPay";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopPaymentWeChatPayApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
