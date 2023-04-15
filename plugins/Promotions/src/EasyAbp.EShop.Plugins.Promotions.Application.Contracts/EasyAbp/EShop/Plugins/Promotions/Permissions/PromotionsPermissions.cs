using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Plugins.Promotions.Permissions;

public class PromotionsPermissions
{
    public const string GroupName = "EasyAbp.EShop.Plugins.Promotions";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(PromotionsPermissions));
    }

    public class Promotion
    {
        public const string Default = GroupName + ".Promotion";
        public const string CrossStore = Default + ".CrossStore";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}
