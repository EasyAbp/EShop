using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Inventory.Permissions
{
    public class InventoryPermissions
    {
        public const string GroupName = "Inventory";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(InventoryPermissions));
        }

        public class Warehouse
        {
            public const string Default = GroupName + ".Warehouse";
            public const string CrossStore = Default + ".CrossStore";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class Stock
        {
            public const string Default = GroupName + ".Stock";
            public const string CrossStore = Default + ".CrossStore";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class Supplier
        {
            public const string Default = GroupName + ".Supplier";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }
    }
}
