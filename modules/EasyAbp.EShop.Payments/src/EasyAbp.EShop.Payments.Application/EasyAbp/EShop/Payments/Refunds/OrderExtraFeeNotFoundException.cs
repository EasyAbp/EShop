using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class OrderExtraFeeNotFoundException : BusinessException
    {
        public OrderExtraFeeNotFoundException(Guid orderId, [NotNull] string name, [CanBeNull] string key) : base(
            PaymentsErrorCodes.OrderExtraFeeNotFound)
        {
            WithData(nameof(orderId), orderId);
            WithData(nameof(name), name);
            WithData(nameof(key), key);
        }
    }
}