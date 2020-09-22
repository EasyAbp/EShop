using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Plugins.Coupons.Permissions
{
    public class CouponsPermissions
    {
        public const string GroupName = "EasyAbp.EShop.Plugins.Coupons";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(CouponsPermissions));
        }
    }
}