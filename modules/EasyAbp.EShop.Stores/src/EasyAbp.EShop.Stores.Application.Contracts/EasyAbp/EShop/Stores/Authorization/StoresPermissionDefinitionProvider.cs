using EasyAbp.EShop.Stores.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Stores.Authorization
{
    public class StoresPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(StoresPermissions.GroupName, L("Permission:Stores"));
            
            var stores = moduleGroup.AddPermission(StoresPermissions.Stores.Default, L("Permission:Store"));
            stores.AddChild(StoresPermissions.Stores.Create, L("Permission:Create"));
            stores.AddChild(StoresPermissions.Stores.Update, L("Permission:Update"));
            stores.AddChild(StoresPermissions.Stores.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<StoresResource>(name);
        }
    }
}