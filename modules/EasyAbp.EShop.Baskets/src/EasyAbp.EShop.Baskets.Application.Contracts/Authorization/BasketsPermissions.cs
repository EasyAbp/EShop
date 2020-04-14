using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Baskets.Authorization
{
    public class BasketsPermissions
    {
        public const string GroupName = "Baskets";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(BasketsPermissions));
        }
    }
}