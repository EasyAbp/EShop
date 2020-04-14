using EasyMall.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyMall.Permissions
{
    public class EasyMallPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(EasyMallPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(EasyMallPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EasyMallResource>(name);
        }
    }
}
