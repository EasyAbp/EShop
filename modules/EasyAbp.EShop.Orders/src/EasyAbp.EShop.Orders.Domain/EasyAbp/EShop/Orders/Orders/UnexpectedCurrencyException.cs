using System;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class UnexpectedCurrencyException : BusinessException
    {
        public UnexpectedCurrencyException(string expectedCurrency) : base(OrdersErrorCodes.UnexpectedCurrency)
        {
            WithData(nameof(expectedCurrency), expectedCurrency);
        }
    }
}