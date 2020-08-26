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

        public class Payments
        {
            public const string Default = GroupName + ".Payment";
            public const string Manage = Default + ".Manage";
            public const string CrossStore = Default + ".CrossStore";
            public const string Create = Default + ".Create";
        }


        public class Refunds
        {
            public const string Default = GroupName + ".Refund";
            public const string Manage = Default + ".Manage";
            public const string CrossStore = Default + ".CrossStore";
            public const string Create = Default + ".Create";
        }

    }
}
