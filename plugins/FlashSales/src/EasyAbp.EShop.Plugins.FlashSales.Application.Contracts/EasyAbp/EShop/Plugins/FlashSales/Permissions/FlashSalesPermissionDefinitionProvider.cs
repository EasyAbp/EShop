using EasyAbp.EShop.Plugins.FlashSales.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Plugins.FlashSales.Permissions;

public class FlashSalesPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(FlashSalesPermissions.GroupName, L("Permission:FlashSales"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FlashSalesResource>(name);
    }
}
