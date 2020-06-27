using System;
using EasyAbp.PaymentService.Refunds;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class Refund : FullAuditedAggregateRoot<Guid>, IRefund
    {
        #region Base properties

        public virtual Guid? TenantId { get; protected set; }
        
        public virtual Guid PaymentId { get; protected set; }
        
        public virtual Guid PaymentItemId { get; protected set; }
        
        [NotNull]
        public virtual string RefundPaymentMethod { get; protected set; }
        
        [NotNull]
        public virtual string ExternalTradingCode { get; protected set; }
        
        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal RefundAmount { get; protected set; }

        [CanBeNull]
        public virtual string CustomerRemark { get; protected set; }
        
        [CanBeNull]
        public virtual string StaffRemark { get; protected set; }

        public virtual DateTime? CompletedTime { get; protected set; }
        
        public virtual DateTime? CanceledTime { get; protected set; }

        #endregion
        
        public virtual Guid? StoreId { get; protected set; }

        private Refund()
        {
            
        }
        
        public void SetStoreId(Guid? storeId)
        {
            StoreId = storeId;
        }
    }
}