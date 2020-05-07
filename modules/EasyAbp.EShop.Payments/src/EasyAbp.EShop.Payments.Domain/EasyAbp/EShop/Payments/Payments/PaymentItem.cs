using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentItem : FullAuditedEntity<Guid>
    {
        [NotNull]
        public virtual string ItemType { get; protected set; }
        
        public virtual Guid ItemKey { get; protected set; }
        
        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal OriginalPaymentAmount { get; protected set; }

        public virtual decimal PaymentDiscount { get; protected set; }
        
        public virtual decimal ActualPaymentAmount { get; protected set; }
        
        public virtual decimal RefundAmount { get; protected set; }

        protected PaymentItem()
        {
        }

        public PaymentItem(
            Guid id,
            [NotNull] string itemType,
            Guid itemKey,
            [NotNull] string currency,
            decimal originalPaymentAmount
        ) :base(id)
        {
            ItemType = itemType;
            ItemKey = itemKey;
            Currency = currency;
            OriginalPaymentAmount = originalPaymentAmount;
        }

        public void CompletePayment(
            decimal paymentDiscount,
            decimal actualPaymentAmount,
            decimal refundAmount)
        {
            PaymentDiscount = paymentDiscount;
            ActualPaymentAmount = actualPaymentAmount;
            RefundAmount = refundAmount;
        }
    }
}
