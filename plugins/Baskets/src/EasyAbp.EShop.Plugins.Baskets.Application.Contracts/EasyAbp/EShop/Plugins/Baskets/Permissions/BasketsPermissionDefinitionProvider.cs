using EasyAbp.EShop.Plugins.Baskets.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Plugins.Baskets.Permissions
{
    public class BasketsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(BasketsPermissions.GroupName, L("Permission:Baskets"));

            var basketItemPermission = myGroup.AddPermission(BasketsPermissions.BasketItem.Default, L("Permission:BasketItem"));
            basketItemPermission.AddChild(BasketsPermissions.BasketItem.Manage, L("Permission:Manage"));
            basketItemPermission.AddChild(BasketsPermissions.BasketItem.Create, L("Permission:Create"));
            basketItemPermission.AddChild(BasketsPermissions.BasketItem.Update, L("Permission:Update"));
            basketItemPermission.AddChild(BasketsPermissions.BasketItem.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<BasketsResource>(name);
        }
    }
}
