using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Plugins.Coupons.Permissions
{
    public class CouponsPermissions
    {
        public const string GroupName = "EasyAbp.EShop.Plugins.Coupons";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(CouponsPermissions));
        }

        public class CouponTemplate
        {
            public const string Default = GroupName + ".CouponTemplate";
            public const string CrossStore = Default + ".CrossStore";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class Coupon
        {
            public const string Default = GroupName + ".Coupon";
            public const string Use = Default + ".Use";
            public const string Manage = Default + ".Manage";
            public const string CrossStore = Default + ".CrossStore";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }
    }
}
