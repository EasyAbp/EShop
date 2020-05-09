using EasyAbp.EShop.Baskets;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.WeChatPay;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Stores;
using EasyMall.MultiTenancy;
using EasyMall.ObjectExtending;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace EasyMall
{
    [DependsOn(
        typeof(EasyMallDomainSharedModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpBackgroundJobsDomainModule),
        typeof(AbpFeatureManagementDomainModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpPermissionManagementDomainIdentityServerModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(AbpTenantManagementDomainModule),
        typeof(EShopBasketsDomainModule),
        typeof(EShopOrdersDomainModule),
        typeof(EShopPaymentsDomainModule),
        typeof(EShopPaymentsWeChatPayDomainModule),
        typeof(EShopProductsDomainModule),
        typeof(EShopStoresDomainModule)
        )]
    public class EasyMallDomainModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            EasyMallDomainObjectExtensions.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });
        }
        
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var resolver = context.ServiceProvider.GetService<IPaymentServiceResolver>();

            resolver.TryRegisterProviderAsync(WeChatPayPaymentServiceProvider.PaymentMethod, typeof(WeChatPayPaymentServiceProvider));
        }
    }
}
