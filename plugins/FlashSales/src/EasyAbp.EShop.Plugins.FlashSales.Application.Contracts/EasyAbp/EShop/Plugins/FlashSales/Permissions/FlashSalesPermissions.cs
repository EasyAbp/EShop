using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Plugins.FlashSales.Permissions;

public class FlashSalesPermissions
{
    public const string GroupName = "FlashSales";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(FlashSalesPermissions));
    }
}
