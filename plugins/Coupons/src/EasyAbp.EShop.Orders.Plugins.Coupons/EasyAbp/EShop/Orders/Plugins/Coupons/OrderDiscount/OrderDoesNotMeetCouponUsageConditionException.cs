using Volo.Abp;

namespace EasyAbp.EShop.Orders.Plugins.Coupons.OrderDiscount
{
    public class OrderDoesNotMeetCouponUsageConditionException : BusinessException
    {
        public OrderDoesNotMeetCouponUsageConditionException() : base("OrderDoesNotMeetCouponUsageCondition",
            "Order does not meet the coupon usage condition.")
        {
        }
    }
}