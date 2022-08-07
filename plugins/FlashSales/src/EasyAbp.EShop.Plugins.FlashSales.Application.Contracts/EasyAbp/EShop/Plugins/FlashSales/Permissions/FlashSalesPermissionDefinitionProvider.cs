using EasyAbp.EShop.Plugins.FlashSales.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Plugins.FlashSales.Permissions;

public class FlashSalesPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(FlashSalesPermissions.GroupName, L("Permission:FlashSales"));

        var flashSalePlanPermission = myGroup.AddPermission(FlashSalesPermissions.FlashSalePlan.Default, L("Permission:FlashSalePlan"));
        flashSalePlanPermission.AddChild(FlashSalesPermissions.FlashSalePlan.Manage, L("Permission:Manage"));
        flashSalePlanPermission.AddChild(FlashSalesPermissions.FlashSalePlan.CrossStore, L("Permission:CrossStore"));
        flashSalePlanPermission.AddChild(FlashSalesPermissions.FlashSalePlan.Create, L("Permission:Create"));
        flashSalePlanPermission.AddChild(FlashSalesPermissions.FlashSalePlan.Update, L("Permission:Update"));
        flashSalePlanPermission.AddChild(FlashSalesPermissions.FlashSalePlan.Delete, L("Permission:Delete"));
        flashSalePlanPermission.AddChild(FlashSalesPermissions.FlashSalePlan.PreOrder, L("Permission:PreOrder"));

        var flashSaleResultPermission = myGroup.AddPermission(FlashSalesPermissions.FlashSaleResult.Default, L("Permission:FlashSaleResult"));
        flashSaleResultPermission.AddChild(FlashSalesPermissions.FlashSaleResult.Manage, L("Permission:Manage"));
        flashSaleResultPermission.AddChild(FlashSalesPermissions.FlashSaleResult.CrossStore, L("Permission:CrossStore"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FlashSalesResource>(name);
    }
}
