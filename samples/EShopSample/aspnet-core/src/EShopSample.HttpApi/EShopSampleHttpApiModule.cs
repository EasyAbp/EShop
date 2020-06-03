using EasyAbp.EShop;
using EasyAbp.PaymentService;
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
        typeof(PaymentServiceHttpApiModule),
        typeof(PaymentServiceWeChatPayHttpApiModule)
    )]
    public class EShopSampleHttpApiModule : AbpModule
    {
        
    }
}
