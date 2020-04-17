using System;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Payment.PaymentRecordOrders
{
    public class PaymentRecordOrder : FullAuditedEntity<Guid>, IMultiTenant, IMultiStore
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid? StoreId { get; protected set; }
        
        public virtual Guid OrderId { get; protected set; }
        
        public virtual decimal OriginalPaymentAmount { get; protected set; }

        public virtual decimal PaymentDiscount { get; protected set; }
        
        public virtual decimal ActualPaymentAmount { get; protected set; }
        
        public virtual decimal RefundAmount { get; protected set; }
    }
}