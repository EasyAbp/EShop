using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Booking.MongoDB;

public static class BookingMongoDbContextExtensions
{
    public static void ConfigureBooking(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
