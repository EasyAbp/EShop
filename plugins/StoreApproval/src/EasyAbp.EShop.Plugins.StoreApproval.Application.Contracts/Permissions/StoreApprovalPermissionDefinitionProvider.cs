using EasyAbp.EShop.Plugins.StoreApproval.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Plugins.StoreApproval.Permissions
{
    public class StoreApprovalPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(StoreApprovalPermissions.GroupName, L("Permission:StoreApproval"));

            var storeApplicationPermission = myGroup.AddPermission(StoreApprovalPermissions.StoreApplication.Default, L("Permission:StoreApplication"));
            storeApplicationPermission.AddChild(StoreApprovalPermissions.StoreApplication.Create, L("Permission:Create"));
            storeApplicationPermission.AddChild(StoreApprovalPermissions.StoreApplication.Update, L("Permission:Update"));
            storeApplicationPermission.AddChild(StoreApprovalPermissions.StoreApplication.Delete, L("Permission:Delete"));
            storeApplicationPermission.AddChild(StoreApprovalPermissions.StoreApplication.Approval, L("Permission:Approval"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<StoreApprovalResource>(name);
        }
    }
}
