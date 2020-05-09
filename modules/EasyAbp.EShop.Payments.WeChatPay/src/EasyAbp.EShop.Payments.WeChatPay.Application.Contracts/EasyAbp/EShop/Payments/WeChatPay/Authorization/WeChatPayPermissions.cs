using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Payments.WeChatPay.Authorization
{
    public class WeChatPayPermissions
    {
        public const string GroupName = "EasyAbp.EShop.Payments.WeChatPay";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(WeChatPayPermissions));
        }
    }
}