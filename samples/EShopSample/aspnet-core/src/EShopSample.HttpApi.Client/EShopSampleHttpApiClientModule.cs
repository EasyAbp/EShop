using EasyAbp.EShop;
using EasyAbp.EShop.Plugins.Baskets;
using EasyAbp.PaymentService;
using EasyAbp.PaymentService.WeChatPay;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace EShopSample
{
    [DependsOn(
        typeof(EShopSampleApplicationContractsModule),
        typeof(AbpAccountHttpApiClientModule),
        typeof(AbpIdentityHttpApiClientModule),
        typeof(AbpPermissionManagementHttpApiClientModule),
        typeof(AbpTenantManagementHttpApiClientModule),
        typeof(AbpFeatureManagementHttpApiClientModule),
        typeof(EShopHttpApiClientModule),
        typeof(EShopPluginsBasketsHttpApiClientModule),
        typeof(PaymentServiceHttpApiClientModule),
        typeof(PaymentServiceWeChatPayHttpApiClientModule)
    )]
    public class EShopSampleHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopSampleApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
