using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Payments.Authorization
{
    public class PaymentsPermissions
    {
        public const string GroupName = "EasyAbp.EShop.Payments";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(PaymentsPermissions));
        }
    }
}