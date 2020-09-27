using Volo.Abp;

namespace EasyAbp.EShop.Orders.Plugins.Coupons.OrderDiscount
{
    public class CouponNotFoundOrHasExpiredException : BusinessException
    {
        public CouponNotFoundOrHasExpiredException() : base("CouponNotFoundOrHasExpired",
            "Coupon not found or has expired.")
        {
        }
    }
}