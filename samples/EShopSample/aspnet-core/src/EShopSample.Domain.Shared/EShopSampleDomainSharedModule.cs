using EasyAbp.EShop;
using EasyAbp.EShop.Plugins.Baskets;
using EasyAbp.EShop.Plugins.Booking;
using EasyAbp.EShop.Plugins.Coupons;
using EasyAbp.PaymentService;
using EasyAbp.PaymentService.Prepayment;
using EasyAbp.PaymentService.WeChatPay;
using EShopSample.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EShopSample
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainSharedModule),
        typeof(AbpBackgroundJobsDomainSharedModule),
        typeof(AbpFeatureManagementDomainSharedModule),
        typeof(AbpIdentityDomainSharedModule),
        typeof(AbpIdentityServerDomainSharedModule),
        typeof(AbpPermissionManagementDomainSharedModule),
        typeof(AbpSettingManagementDomainSharedModule),
        typeof(AbpTenantManagementDomainSharedModule),
        typeof(EShopDomainSharedModule),
        typeof(EShopPluginsBasketsDomainSharedModule),
        typeof(EShopPluginsBookingDomainSharedModule),
        typeof(EShopPluginsCouponsDomainSharedModule),
        typeof(PaymentServiceDomainSharedModule),
        typeof(PaymentServiceWeChatPayDomainSharedModule),
        typeof(PaymentServicePrepaymentDomainSharedModule)
    )]
    public class EShopSampleDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopSampleDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<EShopSampleResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/EShopSample");
            });
        }
    }
}
