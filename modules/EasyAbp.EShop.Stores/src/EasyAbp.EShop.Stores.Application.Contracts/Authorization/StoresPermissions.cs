using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Stores.Authorization
{
    public class StoresPermissions
    {
        public const string GroupName = "Stores";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(StoresPermissions));
        }
    }
}