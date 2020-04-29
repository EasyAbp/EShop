using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Payment.WeChatPay.Authorization
{
    public class WeChatPayPermissions
    {
        public const string GroupName = "EasyAbp.EShop.WeChatPay";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(WeChatPayPermissions));
        }
    }
}