using System;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class CurrencyIsLimitException : BusinessException
    {
        public CurrencyIsLimitException(string expectedCurrency) : base(
            "CurrencyIsLimit",
            $"Only the specified currency {expectedCurrency} is allowed.")
        {
        }
    }
}