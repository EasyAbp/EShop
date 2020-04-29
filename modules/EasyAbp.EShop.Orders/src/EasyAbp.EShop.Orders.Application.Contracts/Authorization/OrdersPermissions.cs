using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Orders.Authorization
{
    public class OrdersPermissions
    {
        public const string GroupName = "EasyAbp.EShop.Orders";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(OrdersPermissions));
        }
    }
}