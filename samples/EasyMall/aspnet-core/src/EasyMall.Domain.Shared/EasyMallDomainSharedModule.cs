using EasyAbp.EShop.Baskets;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Payment;
using EasyAbp.EShop.Payment.WeChatPay;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Stores;
using EasyMall.Localization;
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

namespace EasyMall
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
        typeof(EShopBasketsDomainSharedModule),
        typeof(EShopOrdersDomainSharedModule),
        typeof(EShopPaymentDomainSharedModule),
        typeof(EShopPaymentWeChatPayDomainSharedModule),
        typeof(EShopProductsDomainSharedModule),
        typeof(EShopStoresDomainSharedModule)
        )]
    public class EasyMallDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EasyMallDomainSharedModule>("EasyMall");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<EasyMallResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/EasyMall");
            });
        }
    }
}
