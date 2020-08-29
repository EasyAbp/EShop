using System;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Payments
{
    public class EShopPaymentAlreadyExistsException : BusinessException
    {
        public EShopPaymentAlreadyExistsException(Guid orderId)
            : base(message: $"The order {orderId}'s payment is already exists.")
        {
        }
    }
}