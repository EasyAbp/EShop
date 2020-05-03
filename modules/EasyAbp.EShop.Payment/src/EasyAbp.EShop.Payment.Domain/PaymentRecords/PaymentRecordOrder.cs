using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Payment.PaymentRecords
{
    public class PaymentRecordOrder : FullAuditedEntity<Guid>
    {
        public virtual Guid OrderId { get; protected set; }
        
        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal OriginalPaymentAmount { get; protected set; }

        public virtual decimal PaymentDiscount { get; protected set; }
        
        public virtual decimal ActualPaymentAmount { get; protected set; }
        
        public virtual decimal RefundAmount { get; protected set; }
    }
}