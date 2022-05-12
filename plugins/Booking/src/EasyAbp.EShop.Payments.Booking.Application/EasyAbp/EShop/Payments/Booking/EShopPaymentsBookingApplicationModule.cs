using EasyAbp.EShop.Plugins.Booking;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments.Booking
{
    [DependsOn(
        typeof(EShopPaymentsApplicationModule),
        typeof(EShopPluginsBookingApplicationContractsModule)
    )]
    public class EShopPaymentsBookingApplicationModule : AbpModule
    {
    }
}
