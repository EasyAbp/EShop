using System;
using System.Collections.Generic;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Payments
{
    public class MultiCurrencyNotSupportedException : BusinessException
    {
        public MultiCurrencyNotSupportedException() : base(message: $"Multi-currency is not supported.")
        {
        }
    }
}