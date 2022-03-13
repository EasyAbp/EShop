using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class UserCouponQuantityExceedsLimitException : BusinessException
    {
        public UserCouponQuantityExceedsLimitException(int maxQuantity) : base(CouponsErrorCodes.UserCouponQuantityExceedsLimit)
        {
            WithData(nameof(maxQuantity), maxQuantity);
        }
    }
}