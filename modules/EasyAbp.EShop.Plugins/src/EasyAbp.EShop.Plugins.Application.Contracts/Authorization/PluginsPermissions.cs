using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Plugins.Authorization
{
    public class PluginsPermissions
    {
        public const string GroupName = "EasyAbp.EShop.Plugins";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(PluginsPermissions));
        }
    }
}