using EasyAbp.BookingService;
using EasyAbp.EShop.Orders.Booking.ObjectExtending;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Orders.Booking
{
    [DependsOn(
        typeof(EShopOrdersApplicationModule),
        typeof(EShopOrdersApplicationContractsModule),
        typeof(BookingServiceApplicationContractsModule)
    )]
    public class EShopOrdersBookingApplicationModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            EShopOrdersPluginsBookingObjectExtensions.Configure();
        }
    }
}
