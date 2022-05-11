using EasyAbp.EShop.Plugins.Booking;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments.Plugins.Booking
{
    [DependsOn(
        typeof(EShopPaymentsApplicationModule),
        typeof(EShopPluginsBookingApplicationContractsModule)
    )]
    public class EShopPaymentsPluginsBookingModule : AbpModule
    {
    }
}
