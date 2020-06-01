using System;
using PaymentServiceRefund = EasyAbp.PaymentService.Refunds.Refund;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class Refund : PaymentServiceRefund
    {
        public virtual Guid? StoreId { get; protected set; }

        public void SetStoreId(Guid? storeId)
        {
            StoreId = storeId;
        }
    }
}