using System;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class InvalidCouponOrderIdException : BusinessException
    {
        public InvalidCouponOrderIdException(Guid expectedOrderId, Guid? actualOrderId) : base(CouponsErrorCodes.InvalidCouponOrderId)
        {
            WithData(nameof(expectedOrderId), expectedOrderId);
            WithData(nameof(actualOrderId), actualOrderId);
        }
    }
}