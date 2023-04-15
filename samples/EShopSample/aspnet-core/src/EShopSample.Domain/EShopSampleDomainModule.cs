using EasyAbp.BookingService;
using EasyAbp.EShop;
using EasyAbp.EShop.Orders.Plugins.Promotions;
using EasyAbp.EShop.Plugins.Baskets;
using EasyAbp.EShop.Plugins.Booking;
using EasyAbp.EShop.Plugins.Coupons;
using EasyAbp.EShop.Plugins.FlashSales;
using EasyAbp.EShop.Plugins.Promotions;
using EasyAbp.EShop.Plugins.Promotions.Options;
using EasyAbp.EShop.Plugins.Promotions.PromotionTypes.MinQuantityOrderDiscount;
using EasyAbp.EShop.Plugins.Promotions.PromotionTypes.SimpleProductDiscount;
using EasyAbp.EShop.Products.Plugins.Promotions;
using EasyAbp.PaymentService;
using EasyAbp.PaymentService.Options;
using EasyAbp.PaymentService.Payments;
using EasyAbp.PaymentService.Prepayment;
using EasyAbp.PaymentService.Prepayment.Options;
using EasyAbp.PaymentService.Prepayment.PaymentService;
using EasyAbp.PaymentService.WeChatPay;
using EShopSample.MultiTenancy;
using EShopSample.ObjectExtending;
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

namespace EShopSample
{
    [DependsOn(
        typeof(EShopSampleDomainSharedModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpBackgroundJobsDomainModule),
        typeof(AbpFeatureManagementDomainModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpPermissionManagementDomainIdentityServerModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(AbpTenantManagementDomainModule),
        typeof(EShopDomainModule),
        typeof(EShopPluginsBasketsDomainModule),
        typeof(EShopPluginsBookingDomainModule),
        typeof(EShopPluginsCouponsDomainModule),
        typeof(EShopPluginsFlashSalesDomainModule),
        typeof(EShopPluginsPromotionsDomainModule),
        typeof(EShopProductsPluginsPromotionsDomainModule),
        typeof(EShopOrdersPluginsPromotionsDomainModule),
        typeof(PaymentServiceDomainModule),
        typeof(PaymentServiceWeChatPayDomainModule),
        typeof(PaymentServicePrepaymentDomainModule),
        typeof(BookingServiceDomainModule)
    )]
    public class EShopSampleDomainModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            EShopSampleDomainObjectExtensions.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options => { options.IsEnabled = MultiTenancyConsts.IsEnabled; });

            ConfigurePaymentService();
            ConfigurePaymentServicePrepayment();
            ConfigureEShopPromotions();
        }

        private void ConfigurePaymentService()
        {
            Configure<PaymentServiceOptions>(options =>
            {
                options.Providers.Configure<FreePaymentServiceProvider>(FreePaymentServiceProvider.PaymentMethod);
                options.Providers.Configure<WeChatPayPaymentServiceProvider>(WeChatPayPaymentServiceProvider
                    .PaymentMethod);
                options.Providers.Configure<PrepaymentPaymentServiceProvider>(PrepaymentPaymentServiceProvider
                    .PaymentMethod);
            });
        }

        private void ConfigurePaymentServicePrepayment()
        {
            Configure<PaymentServicePrepaymentOptions>(options =>
            {
                options.AccountGroups.Configure<DefaultAccountGroup>(accountGroup =>
                {
                    accountGroup.Currency = "USD";
                });
            });
        }

        private void ConfigureEShopPromotions()
        {
            Configure<EShopPluginsPromotionsOptions>(options =>
            {
                options.PromotionTypes.AddSimpleProductDiscountPromotionType();
                options.PromotionTypes.AddMinQuantityOrderDiscountPromotionType();
            });
        }
    }
}