using Volo.Abp;
using EasyAbp.EShop.Plugins.Coupons;

namespace EasyAbp.EShop.Orders.Plugins.Coupons.OrderDiscount
{
    public class CouponNotFoundOrHasExpiredException : BusinessException
    {
        public CouponNotFoundOrHasExpiredException() : base(CouponsErrorCodes.CouponNotFoundOrHasExpired)
        {
        }
    }
}