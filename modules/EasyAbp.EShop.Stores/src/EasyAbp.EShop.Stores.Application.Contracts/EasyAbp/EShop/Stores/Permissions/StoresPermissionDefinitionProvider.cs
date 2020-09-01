using EasyAbp.EShop.Stores.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Stores.Permissions
{
    public class StoresPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(StoresPermissions.GroupName, L("Permission:Stores"));
            
            var stores = myGroup.AddPermission(StoresPermissions.Stores.Default, L("Permission:Store"));
            stores.AddChild(StoresPermissions.Stores.CrossStore, L("Permission:CrossStore"));
            stores.AddChild(StoresPermissions.Stores.Manage, L("Permission:Manage"));
            stores.AddChild(StoresPermissions.Stores.Create, L("Permission:Create"));
            stores.AddChild(StoresPermissions.Stores.Update, L("Permission:Update"));
            stores.AddChild(StoresPermissions.Stores.Delete, L("Permission:Delete"), MultiTenancySides.Host);

            var transactionPermission = myGroup.AddPermission(StoresPermissions.Transaction.Default, L("Permission:Transaction"));
            transactionPermission.AddChild(StoresPermissions.Transaction.Create, L("Permission:Create"));
            transactionPermission.AddChild(StoresPermissions.Transaction.Update, L("Permission:Update"));
            transactionPermission.AddChild(StoresPermissions.Transaction.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<StoresResource>(name);
        }
    }
}
