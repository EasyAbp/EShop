using System;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class AnotherRefundTaskIsOnGoingException : BusinessException
    {
        public AnotherRefundTaskIsOnGoingException(Guid id) : base(PaymentsErrorCodes.AnotherRefundTaskIsOnGoing)
        {
            WithData(nameof(id), id);
        }
    }
}