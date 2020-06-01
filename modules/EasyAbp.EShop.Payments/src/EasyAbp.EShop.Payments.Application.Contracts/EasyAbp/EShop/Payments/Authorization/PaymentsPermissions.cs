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
            public const string Default = GroupName + ".Payments";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }


        public class Refunds
        {
            public const string Default = GroupName + ".Refunds";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

    }
}
