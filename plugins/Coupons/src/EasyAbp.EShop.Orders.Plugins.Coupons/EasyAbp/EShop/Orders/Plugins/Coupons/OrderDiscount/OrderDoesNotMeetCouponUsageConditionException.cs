using Volo.Abp;
using EasyAbp.EShop.Plugins.Coupons;

namespace EasyAbp.EShop.Orders.Plugins.Coupons.OrderDiscount
{
    public class OrderDoesNotMeetCouponUsageConditionException : BusinessException
    {
        public OrderDoesNotMeetCouponUsageConditionException() : base(CouponsErrorCodes.OrderDoesNotMeetCouponUsageCondition)
        {
        }
    }
}