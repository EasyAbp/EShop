using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Products.Permissions
{
    public class ProductsPermissions
    {
        public const string GroupName = "EasyAbp.EShop.Products";

        public class Categories
        {
            public const string Default = GroupName + ".Category";
            public const string CrossStore = Default + ".CrossStore";
            public const string Manage = Default + ".Manage";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string ShowHidden = Default + ".ShowHidden";
        }

        public class Products
        {
            public const string Default = GroupName + ".Product";
            public const string CrossStore = Default + ".CrossStore";
            public const string Manage = Default + ".Manage";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }
        
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(ProductsPermissions));
        }

        public class ProductInventory
        {
            public const string Default = GroupName + ".ProductInventory";
            public const string CrossStore = Default + ".CrossStore";
            public const string Update = Default + ".Update";
        }

    }
}
