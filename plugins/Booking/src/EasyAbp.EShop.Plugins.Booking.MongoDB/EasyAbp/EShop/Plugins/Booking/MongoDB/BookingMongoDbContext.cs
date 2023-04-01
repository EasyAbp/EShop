using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Booking.MongoDB;

[ConnectionStringName(BookingDbProperties.ConnectionStringName)]
public class BookingMongoDbContext : AbpMongoDbContext, IBookingMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureEShopPluginsBooking();
    }
}
