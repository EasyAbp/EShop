using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Products.Permissions
{
    public class ProductsPluginsFlashSalesPermissions
    {
        public const string GroupName = "EasyAbp.EShop.Products.Plugins.FlashSales";

        public class FlashSaleInventory
        {
            public const string Default = GroupName + ".FlashSaleInventory";
            public const string Reduce = Default + ".Reduce";
            public const string Increase = Default + ".Increase";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(ProductsPluginsFlashSalesPermissions));
        }
    }
}
