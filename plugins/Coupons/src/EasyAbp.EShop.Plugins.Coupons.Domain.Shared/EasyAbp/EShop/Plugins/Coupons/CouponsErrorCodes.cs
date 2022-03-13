namespace EasyAbp.EShop.Plugins.Coupons
{
    public static class CouponsErrorCodes
    {
        public const string CouponNotFoundOrHasExpired = "EasyAbp.EShop.Plugins.Coupons:CouponNotFoundOrHasExpired";
        public const string CouponTemplateNotFoundOrUnavailable = "EasyAbp.EShop.Plugins.Coupons:CouponTemplateNotFoundOrUnavailable";
        public const string OrderDoesNotMeetCouponUsageCondition = "EasyAbp.EShop.Plugins.Coupons:OrderDoesNotMeetCouponUsageCondition";
        public const string CouponHasBeenOccupied = "EasyAbp.EShop.Plugins.Coupons:CouponHasBeenOccupied";
        public const string InvalidCouponOrderId = "EasyAbp.EShop.Plugins.Coupons:InvalidCouponOrderId";
        public const string UserCouponQuantityExceedsLimit = "EasyAbp.EShop.Plugins.Coupons:UserCouponQuantityExceedsLimit";
    }
}
