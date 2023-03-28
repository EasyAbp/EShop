using EasyAbp.EShop.Plugins.Promotions.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Plugins.Promotions.Permissions;

public class PromotionsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(PromotionsPermissions.GroupName, L("Permission:Promotions"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PromotionsResource>(name);
    }
}
