using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Plugins.FlashSales.Permissions;

public class FlashSalesPermissions
{
    public const string GroupName = "EasyAbp.EShop.Plugins.FlashSales";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(FlashSalesPermissions));
    }

    public class FlashSalesPlan
    {
        public const string Default = GroupName + ".FlashSalesPlan";
        public const string Manage = Default + ".Manage";
        public const string CrossStore = Default + ".CrossStore";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class FlashSalesResult
    {
        public const string Default = GroupName + ".FlashSalesResult";
        public const string Manage = Default + ".Manage";
    }
}
