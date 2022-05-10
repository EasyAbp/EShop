using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Booking;

[DependsOn(
    typeof(BookingApplicationModule),
    typeof(BookingDomainTestModule)
    )]
public class BookingApplicationTestModule : AbpModule
{

}
