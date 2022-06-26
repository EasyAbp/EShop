using EasyAbp.EShop.Plugins.FlashSales.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Plugins.FlashSales.Permissions;

public class FlashSalesPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(FlashSalesPermissions.GroupName, L("Permission:FlashSales"));

        var flashSalesPlanPermission = myGroup.AddPermission(FlashSalesPermissions.FlashSalesPlan.Default, L("Permission:FlashSalesPlan"));
        flashSalesPlanPermission.AddChild(FlashSalesPermissions.FlashSalesPlan.Manage, L("Permission:Manage"));
        flashSalesPlanPermission.AddChild(FlashSalesPermissions.FlashSalesPlan.Create, L("Permission:Create"));
        flashSalesPlanPermission.AddChild(FlashSalesPermissions.FlashSalesPlan.Update, L("Permission:Update"));
        flashSalesPlanPermission.AddChild(FlashSalesPermissions.FlashSalesPlan.Delete, L("Permission:Delete"));

        var flashSalesResultPermission = myGroup.AddPermission(FlashSalesPermissions.FlashSalesResult.Default, L("Permission:FlashSalesResult"));
        flashSalesResultPermission.AddChild(FlashSalesPermissions.FlashSalesResult.Manage, L("Permission:Manage"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FlashSalesResource>(name);
    }
}
