using System;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class CurrencyIsLimitException : BusinessException
    {
        public CurrencyIsLimitException(string expectedCurrency) : base(OrdersErrorCodes.CurrencyIsLimit)
        {
            WithData(nameof(expectedCurrency), expectedCurrency);
        }
    }
}