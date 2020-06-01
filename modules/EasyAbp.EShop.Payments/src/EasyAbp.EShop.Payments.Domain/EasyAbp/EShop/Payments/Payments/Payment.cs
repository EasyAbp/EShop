using System;
using PaymentServicePayment = EasyAbp.PaymentService.Payments.Payment;

namespace EasyAbp.EShop.Payments.Payments
{
    public class Payment : PaymentServicePayment
    {
        public virtual Guid? StoreId { get; protected set; }

        public void SetStoreId(Guid? storeId)
        {
            StoreId = storeId;
        }
    }
}