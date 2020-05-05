using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Orders.Authorization
{
    public class OrdersPermissions
    {
        public const string GroupName = "EasyAbp.EShop.Orders";

        public class Orders
        {
            public const string Default = GroupName + ".Order";
            public const string Manage = Default + ".Manage";
            public const string CrossStore = Default + ".CrossStore";
            public const string Create = Default + ".Create";
            public const string ConfirmReceipt = Default + ".ConfirmReceipt";
            public const string RequestCancellation = Default + ".RequestCancellation";
            public const string Cancel = Default + ".Cancel";
        }
        
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(OrdersPermissions));
        }
    }
}