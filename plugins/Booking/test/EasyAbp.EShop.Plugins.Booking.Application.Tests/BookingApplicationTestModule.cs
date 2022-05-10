using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Booking;

[DependsOn(
    typeof(EShopPluginsBookingApplicationModule),
    typeof(BookingDomainTestModule)
    )]
public class BookingApplicationTestModule : AbpModule
{

}
