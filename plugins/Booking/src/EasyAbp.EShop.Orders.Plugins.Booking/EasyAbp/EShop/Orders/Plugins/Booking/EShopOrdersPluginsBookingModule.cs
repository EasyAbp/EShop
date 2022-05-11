using EasyAbp.EShop.Orders.Plugins.Booking.ObjectExtending;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Orders.Plugins.Booking
{
    [DependsOn(
        typeof(EShopOrdersApplicationModule),
        typeof(EShopOrdersApplicationContractsModule)
    )]
    public class EShopOrdersPluginsBookingModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            EShopOrdersPluginsBookingObjectExtensions.Configure();
        }
    }
}
