using System;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Payments
{
    public class CurrencyNotSupportedException : BusinessException
    {
        public CurrencyNotSupportedException(string paymentMethod, string currency) : base(
            message: $"Payment method {paymentMethod} does not support currency: {currency}")
        {
        }
        
        public CurrencyNotSupportedException(string paymentMethod, string currency, Guid storeId) : base(
            message: $"Payment method {paymentMethod} in store {storeId} does not support currency: {currency}")
        {
        }
    }
}