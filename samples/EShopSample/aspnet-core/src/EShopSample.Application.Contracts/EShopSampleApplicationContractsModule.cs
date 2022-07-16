using EasyAbp.EShop;
using EasyAbp.EShop.Plugins.Baskets;
using EasyAbp.EShop.Plugins.Booking;
using EasyAbp.EShop.Plugins.Coupons;
using EasyAbp.EShop.Plugins.FlashSales;
using EasyAbp.EShop.Products.Plugins.FlashSales;
using EasyAbp.PaymentService;
using EasyAbp.PaymentService.Prepayment;
using EasyAbp.PaymentService.WeChatPay;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace EShopSample
{
    [DependsOn(
        typeof(EShopSampleDomainSharedModule),
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(AbpObjectExtendingModule),
        typeof(EShopApplicationContractsModule),
        typeof(EShopPluginsBasketsApplicationContractsModule),
        typeof(EShopPluginsBookingApplicationContractsModule),
        typeof(EShopPluginsCouponsApplicationContractsModule),
        typeof(EShopPluginsFlashSalesApplicationContractsModule),
        typeof(EShopProductsPluginsFlashSalesApplicationContractsModule),
        typeof(PaymentServiceApplicationContractsModule),
        typeof(PaymentServiceWeChatPayApplicationContractsModule),
        typeof(PaymentServicePrepaymentApplicationContractsModule)
    )]
    public class EShopSampleApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            EShopSampleDtoExtensions.Configure();
        }
    }
}
