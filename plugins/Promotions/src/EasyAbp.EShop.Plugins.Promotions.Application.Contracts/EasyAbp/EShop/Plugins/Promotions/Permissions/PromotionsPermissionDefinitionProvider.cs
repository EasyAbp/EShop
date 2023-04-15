using EasyAbp.EShop.Plugins.Promotions.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Plugins.Promotions.Permissions;

public class PromotionsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(PromotionsPermissions.GroupName, L("Permission:Promotions"));

        var promotionPermission = myGroup.AddPermission(PromotionsPermissions.Promotion.Default, L("Permission:Promotion"));
        promotionPermission.AddChild(PromotionsPermissions.Promotion.CrossStore, L("Permission:CrossStore"));
        promotionPermission.AddChild(PromotionsPermissions.Promotion.Create, L("Permission:Create"));
        promotionPermission.AddChild(PromotionsPermissions.Promotion.Update, L("Permission:Update"));
        promotionPermission.AddChild(PromotionsPermissions.Promotion.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PromotionsResource>(name);
    }
}
