using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Stores.Authorization
{
    public class StoresPermissions
    {
        public const string GroupName = "EasyAbp.EShop.Stores";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(StoresPermissions));
        }
        
        public class Stores
        {
            public const string Default = GroupName + ".Store";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }
    }
}