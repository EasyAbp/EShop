using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Booking;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(BookingDomainSharedModule)
)]
public class BookingDomainModule : AbpModule
{

}
