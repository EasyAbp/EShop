using Volo.Abp;
using EasyAbp.EShop.Plugins.Coupons;

namespace EasyAbp.EShop.Orders.Plugins.Coupons.OrderDiscount
{
    public class CouponTemplateNotFoundOrUnavailableException : BusinessException
    {
        public CouponTemplateNotFoundOrUnavailableException() : base(CouponsErrorCodes.CouponTemplateNotFoundOrUnavailable)
        {
        }
    }
}