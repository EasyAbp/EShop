using System;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class InvalidCouponOrderIdException : BusinessException
    {
        public InvalidCouponOrderIdException(Guid expectedOrderId, Guid? actualOrderId) : base("InvalidCouponOrderId",
            $"The expected order ID is {expectedOrderId}, but the actual order ID is {actualOrderId}.")
        {
        }
    }
}