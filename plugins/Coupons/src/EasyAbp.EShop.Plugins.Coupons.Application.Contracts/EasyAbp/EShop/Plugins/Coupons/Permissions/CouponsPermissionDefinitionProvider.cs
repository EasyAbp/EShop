using EasyAbp.EShop.Plugins.Coupons.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Plugins.Coupons.Permissions
{
    public class CouponsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(CouponsPermissions.GroupName, L("Permission:Coupons"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CouponsResource>(name);
        }
    }
}