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
        productAssetPermission.AddChild(BookingPermissions.ProductAsset.Manage, L("Permission:Manage"));
        productAssetPermission.AddChild(BookingPermissions.ProductAsset.Create, L("Permission:Create"));
        productAssetPermission.AddChild(BookingPermissions.ProductAsset.Update, L("Permission:Update"));
        productAssetPermission.AddChild(BookingPermissions.ProductAsset.Delete, L("Permission:Delete"));

        var productAssetCategoryPermission = myGroup.AddPermission(BookingPermissions.ProductAssetCategory.Default, L("Permission:ProductAssetCategory"));
        productAssetCategoryPermission.AddChild(BookingPermissions.ProductAssetCategory.Manage, L("Permission:Manage"));
        productAssetCategoryPermission.AddChild(BookingPermissions.ProductAssetCategory.Create, L("Permission:Create"));
        productAssetCategoryPermission.AddChild(BookingPermissions.ProductAssetCategory.Update, L("Permission:Update"));
        productAssetCategoryPermission.AddChild(BookingPermissions.ProductAssetCategory.Delete, L("Permission:Delete"));

        var grantedStorePermission = myGroup.AddPermission(BookingPermissions.GrantedStore.Default, L("Permission:GrantedStore"));
        grantedStorePermission.AddChild(BookingPermissions.GrantedStore.Create, L("Permission:Create"));
        grantedStorePermission.AddChild(BookingPermissions.GrantedStore.Update, L("Permission:Update"));
        grantedStorePermission.AddChild(BookingPermissions.GrantedStore.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookingResource>(name);
    }
}
