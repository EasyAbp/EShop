using EasyAbp.EShop.Stores.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Stores.Authorization
{
    public class StoresPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            //var moduleGroup = context.AddGroup(StoresPermissions.GroupName, L("Permission:Stores"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<StoresResource>(name);
        }
    }
}