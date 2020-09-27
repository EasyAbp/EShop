using EasyAbp.EShop.Plugins.Coupons.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Plugins.Coupons.Permissions
{
    public class CouponsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(CouponsPermissions.GroupName, L("Permission:Coupons"));

            var couponTemplatePermission = myGroup.AddPermission(CouponsPermissions.CouponTemplate.Default, L("Permission:CouponTemplate"));
            couponTemplatePermission.AddChild(CouponsPermissions.CouponTemplate.Create, L("Permission:Create"));
            couponTemplatePermission.AddChild(CouponsPermissions.CouponTemplate.Update, L("Permission:Update"));
            couponTemplatePermission.AddChild(CouponsPermissions.CouponTemplate.Delete, L("Permission:Delete"));

            var couponPermission = myGroup.AddPermission(CouponsPermissions.Coupon.Default, L("Permission:Coupon"));
            couponPermission.AddChild(CouponsPermissions.Coupon.Use, L("Permission:Use"));
            couponPermission.AddChild(CouponsPermissions.Coupon.Manage, L("Permission:Manage"));
            couponPermission.AddChild(CouponsPermissions.Coupon.Create, L("Permission:Create"));
            couponPermission.AddChild(CouponsPermissions.Coupon.Update, L("Permission:Update"));
            couponPermission.AddChild(CouponsPermissions.Coupon.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CouponsResource>(name);
        }
    }
}
