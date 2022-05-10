namespace EasyAbp.EShop.Plugins.Booking;

public static class BookingDbProperties
{
    public static string DbTablePrefix { get; set; } = "EasyAbpEShopPluginsBooking";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "EasyAbpEShopPluginsBooking";
}
