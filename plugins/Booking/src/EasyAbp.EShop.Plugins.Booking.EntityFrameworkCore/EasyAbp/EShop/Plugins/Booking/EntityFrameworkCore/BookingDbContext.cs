using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore;

[ConnectionStringName(BookingDbProperties.ConnectionStringName)]
public class BookingDbContext : AbpDbContext<BookingDbContext>, IBookingDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public BookingDbContext(DbContextOptions<BookingDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureBooking();
    }
}
