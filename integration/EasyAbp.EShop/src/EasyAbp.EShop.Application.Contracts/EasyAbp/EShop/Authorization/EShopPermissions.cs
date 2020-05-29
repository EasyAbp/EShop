using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Authorization
{
    public class EShopPermissions
    {
        public const string GroupName = "EasyAbp.EShop";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(EShopPermissions));
        }
    }
}