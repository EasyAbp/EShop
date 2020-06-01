using EShopSample.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EShopSample.Permissions
{
    public class EShopSamplePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(EShopSamplePermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(EShopSamplePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EShopSampleResource>(name);
        }
    }
}
