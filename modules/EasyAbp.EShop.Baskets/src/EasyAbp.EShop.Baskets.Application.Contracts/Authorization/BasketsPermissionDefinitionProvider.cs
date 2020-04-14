using EasyAbp.EShop.Baskets.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Baskets.Authorization
{
    public class BasketsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            //var moduleGroup = context.AddGroup(BasketsPermissions.GroupName, L("Permission:Baskets"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<BasketsResource>(name);
        }
    }
}