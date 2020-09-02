using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Stores.Permissions
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
            public const string CrossStore = Default + ".CrossStore";
            public const string Manage = Default + ".Manage";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public class Transaction
        {
            public const string Default = GroupName + ".Transaction";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }
    }
}
