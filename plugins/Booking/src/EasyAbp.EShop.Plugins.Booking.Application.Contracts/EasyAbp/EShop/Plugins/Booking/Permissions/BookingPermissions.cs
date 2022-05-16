using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Plugins.Booking.Permissions;

public class BookingPermissions
{
    public const string GroupName = "EasyAbp.EShop.Plugins.Booking";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(BookingPermissions));
    }

        public class ProductAsset
        {
            public const string Default = GroupName + ".ProductAsset";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class ProductAssetCategory
        {
            public const string Default = GroupName + ".ProductAssetCategory";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }
}
