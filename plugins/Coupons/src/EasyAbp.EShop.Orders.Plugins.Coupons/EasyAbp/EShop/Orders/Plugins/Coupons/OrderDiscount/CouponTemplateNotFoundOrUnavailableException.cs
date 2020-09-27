using Volo.Abp;

namespace EasyAbp.EShop.Orders.Plugins.Coupons.OrderDiscount
{
    public class CouponTemplateNotFoundOrUnavailableException : BusinessException
    {
        public CouponTemplateNotFoundOrUnavailableException() : base("CouponTemplateNotFoundOrUnavailable",
            "Coupon template not found or unavailable.")
        {
        }
    }
}