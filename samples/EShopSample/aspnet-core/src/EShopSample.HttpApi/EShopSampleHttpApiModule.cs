using EasyAbp.EShop;
using EasyAbp.EShop.Plugins.Baskets;
using EasyAbp.PaymentService;
using EasyAbp.PaymentService.Prepayment;
using EasyAbp.PaymentService.WeChatPay;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.TenantManagement;

namespace EShopSample
{
    [DependsOn(
        typeof(EShopSampleApplicationContractsModule),
        typeof(AbpAccountHttpApiModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpTenantManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule),
        typeof(EShopHttpApiModule),
        typeof(EShopPluginsBasketsHttpApiModule),
        typeof(PaymentServiceHttpApiModule),
        typeof(PaymentServiceWeChatPayHttpApiModule),
        typeof(PaymentServicePrepaymentHttpApiModule)
    )]
    public class EShopSampleHttpApiModule : AbpModule
    {
        
    }
}
