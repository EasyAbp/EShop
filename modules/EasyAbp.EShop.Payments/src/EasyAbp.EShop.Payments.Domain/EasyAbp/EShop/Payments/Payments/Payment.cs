using System;
using System.Collections.Generic;
using EasyAbp.PaymentService.Payments;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Payments.Payments
{
    public class Payment : FullAuditedAggregateRoot<Guid>, IPayment, IMultiTenant
    {
        #region Base properties
        
        public virtual Guid? TenantId { get; protected set; }
        
        public virtual Guid UserId { get; protected set; }
        
        [NotNull]
        public virtual string PaymentMethod { get; protected set; }
        
        [CanBeNull]
        public virtual string PayeeAccount { get; protected set; }
        
        [CanBeNull]
        public virtual string ExternalTradingCode { get; protected set; }
        
        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal OriginalPaymentAmount { get; protected set; }

        public virtual decimal PaymentDiscount { get; protected set; }
        
        public virtual decimal ActualPaymentAmount { get; protected set; }
        
        public virtual decimal RefundAmount { get; protected set; }
        
        public virtual decimal PendingRefundAmount { get; protected set; }

        public virtual DateTime? CompletionTime { get; protected set; }
        
        public virtual DateTime? CancelledTime { get; protected set; }
        
        public virtual List<PaymentItem> PaymentItems { get; protected set; }
        
        #endregion

        public virtual Guid? StoreId { get; protected set; }

        private Payment()
        {
            
        }
        
        public void SetStoreId(Guid? storeId)
        {
            StoreId = storeId;
        }
    }
}