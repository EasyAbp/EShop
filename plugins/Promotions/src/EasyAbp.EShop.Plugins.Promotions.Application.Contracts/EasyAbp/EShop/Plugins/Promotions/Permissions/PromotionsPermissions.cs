using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Plugins.Promotions.Permissions;

public class PromotionsPermissions
{
    public const string GroupName = "EasyAbp.EShop.Plugins.Promotions";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(PromotionsPermissions));
    }
}
