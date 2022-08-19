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
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.VirtualFileSystem;

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
        typeof(EShopPluginsBookingHttpApiClientModule),
        typeof(EShopPluginsCouponsHttpApiClientModule),
        typeof(EShopPluginsFlashSalesHttpApiClientModule),
        typeof(EShopProductsPluginsFlashSalesHttpApiClientModule),
        typeof(PaymentServiceHttpApiClientModule),
        typeof(PaymentServiceWeChatPayHttpApiClientModule),
        typeof(PaymentServicePrepaymentHttpApiClientModule),
        typeof(BookingServiceHttpApiClientModule)
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
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopSampleApplicationContractsModule>();
            });
        }
    }
}
