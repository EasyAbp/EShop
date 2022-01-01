using EasyAbp.EShop.Plugins.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Plugins.Authorization
{
    public class PluginsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            //var moduleGroup = context.AddGroup(PluginsPermissions.GroupName, L("Permission:Plugins"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PluginsResource>(name);
        }
    }
}