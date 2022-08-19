using EasyAbp.BookingService;
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
        typeof(EShopPluginsBookingHttpApiModule),
        typeof(EShopPluginsCouponsHttpApiModule),
        typeof(EShopPluginsFlashSalesHttpApiModule),
        typeof(EShopProductsPluginsFlashSalesHttpApiModule),
        typeof(PaymentServiceHttpApiModule),
        typeof(PaymentServiceWeChatPayHttpApiModule),
        typeof(PaymentServicePrepaymentHttpApiModule),
        typeof(BookingServiceHttpApiModule)
    )]
    public class EShopSampleHttpApiModule : AbpModule
    {
        
    }
}
