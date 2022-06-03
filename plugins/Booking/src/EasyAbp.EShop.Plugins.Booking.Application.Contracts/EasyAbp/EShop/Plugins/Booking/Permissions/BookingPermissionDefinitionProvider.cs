using EasyAbp.EShop.Plugins.Booking.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Plugins.Booking.Permissions;

public class BookingPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BookingPermissions.GroupName, L("Permission:Booking"));

        var productAssetPermission = myGroup.AddPermission(BookingPermissions.ProductAsset.Default, L("Permission:ProductAsset"));
        productAssetPermission.AddChild(BookingPermissions.ProductAsset.Create, L("Permission:Create"));
        productAssetPermission.AddChild(BookingPermissions.ProductAsset.Update, L("Permission:Update"));
        productAssetPermission.AddChild(BookingPermissions.ProductAsset.Delete, L("Permission:Delete"));

        var productAssetCategoryPermission = myGroup.AddPermission(BookingPermissions.ProductAssetCategory.Default, L("Permission:ProductAssetCategory"));
        productAssetCategoryPermission.AddChild(BookingPermissions.ProductAssetCategory.Create, L("Permission:Create"));
        productAssetCategoryPermission.AddChild(BookingPermissions.ProductAssetCategory.Update, L("Permission:Update"));
        productAssetCategoryPermission.AddChild(BookingPermissions.ProductAssetCategory.Delete, L("Permission:Delete"));

        var storeAssetCategoryPermission = myGroup.AddPermission(BookingPermissions.StoreAssetCategory.Default, L("Permission:StoreAssetCategory"));
        storeAssetCategoryPermission.AddChild(BookingPermissions.StoreAssetCategory.Create, L("Permission:Create"));
        storeAssetCategoryPermission.AddChild(BookingPermissions.StoreAssetCategory.Update, L("Permission:Update"));
        storeAssetCategoryPermission.AddChild(BookingPermissions.StoreAssetCategory.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookingResource>(name);
    }
}
