using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Plugins.Baskets.Booking.ObjectExtending;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Baskets.Booking;

[DependsOn(
    typeof(AbpCachingModule),
    typeof(EShopPluginsBasketsApplicationModule),
    typeof(EShopOrdersApplicationContractsModule)
)]
public class EShopPluginsBasketsBookingApplicationModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        EShopPluginsBasketsBookingObjectExtensions.Configure();
    }
}