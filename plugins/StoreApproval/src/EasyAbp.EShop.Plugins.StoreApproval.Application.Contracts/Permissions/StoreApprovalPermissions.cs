using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Plugins.StoreApproval.Permissions
{
    public class StoreApprovalPermissions
    {
        public const string GroupName = "StoreApproval";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(StoreApprovalPermissions));
        }

        public class StoreApplication
        {
            public const string Default = GroupName + ".StoreApplication";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
            public const string Approval = Default + ".Approval";
        }
    }
}
