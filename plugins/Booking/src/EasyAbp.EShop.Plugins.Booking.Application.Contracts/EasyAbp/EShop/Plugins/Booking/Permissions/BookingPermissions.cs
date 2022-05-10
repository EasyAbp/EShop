using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Plugins.Booking.Permissions;

public class BookingPermissions
{
    public const string GroupName = "EasyAbp.EShop.Plugins.Booking";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(BookingPermissions));
    }
}
