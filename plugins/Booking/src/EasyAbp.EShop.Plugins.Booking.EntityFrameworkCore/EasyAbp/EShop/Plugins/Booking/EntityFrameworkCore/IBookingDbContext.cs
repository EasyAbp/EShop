using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore;

[ConnectionStringName(BookingDbProperties.ConnectionStringName)]
public interface IBookingDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
