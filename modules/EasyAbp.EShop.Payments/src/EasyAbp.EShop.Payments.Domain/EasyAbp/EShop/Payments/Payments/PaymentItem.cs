using System;
using EasyAbp.PaymentService.Payments;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentItem : FullAuditedEntity<Guid>, IPaymentItem
    {
        #region Base properties

        [NotNull]
        public virtual string ItemType { get; protected set; }
        
        public virtual Guid ItemKey { get; protected set; }
        
        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal OriginalPaymentAmount { get; protected set; }

        public virtual decimal PaymentDiscount { get; protected set; }
        
        public virtual decimal ActualPaymentAmount { get; protected set; }
        
        public virtual decimal RefundAmount { get; protected set; }
        
        public virtual decimal PendingRefundAmount { get; protected set; }

        #endregion
        
        private PaymentItem()
        {
            
        }
    }
}
