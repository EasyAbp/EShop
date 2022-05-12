using EasyAbp.EShop.Orders.Booking.ObjectExtending;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Orders.Booking
{
    [DependsOn(
        typeof(EShopOrdersApplicationModule),
        typeof(EShopOrdersApplicationContractsModule)
    )]
    public class EShopOrdersBookingApplicationModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            EShopOrdersPluginsBookingObjectExtensions.Configure();
        }
    }
}
