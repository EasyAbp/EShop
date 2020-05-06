using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Payments.PaymentsRecords
{
    public class PaymentsRecordOrder : FullAuditedEntity<Guid>
    {
        public virtual Guid OrderId { get; protected set; }
        
        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal OriginalPaymentsAmount { get; protected set; }

        public virtual decimal PaymentsDiscount { get; protected set; }
        
        public virtual decimal ActualPaymentsAmount { get; protected set; }
        
        public virtual decimal RefundAmount { get; protected set; }
    }
}