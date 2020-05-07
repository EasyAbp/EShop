using System;
using System.Collections.Generic;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentItemNotPayableException : BusinessException
    {
        public PaymentItemNotPayableException(Guid itemKey) : base(
            message: $"Payment item ({itemKey}) is not payable")
        {
        }

        public PaymentItemNotPayableException(IEnumerable<Guid> itemKeys) : base(
            message: $"Payment item ({itemKeys.JoinAsString(", ")}) is not payable")
        {
        }
    }
}