using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class UserCouponQuantityExceedsLimitException : BusinessException
    {
        public UserCouponQuantityExceedsLimitException(int maxQuantity) : base("UserCouponQuantityExceedsLimit",
            $"User's coupon quantity exceeds the limit: {maxQuantity}.")
        {
        }
    }
}