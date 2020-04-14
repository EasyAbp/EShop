using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Payment.Authorization
{
    public class PaymentPermissions
    {
        public const string GroupName = "Payment";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(PaymentPermissions));
        }
    }
}