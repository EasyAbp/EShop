using EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Booking;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(BookingEntityFrameworkCoreTestModule)
    )]
public class BookingDomainTestModule : AbpModule
{

}
