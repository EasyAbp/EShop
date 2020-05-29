using EasyAbp.EShop.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Authorization
{
    public class EShopPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            //var moduleGroup = context.AddGroup(EShopPermissions.GroupName, L("Permission:EShop"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EShopResource>(name);
        }
    }
}