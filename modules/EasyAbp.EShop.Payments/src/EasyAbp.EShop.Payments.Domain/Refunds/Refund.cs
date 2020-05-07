using System;
using EasyAbp.EShop.Stores.Stores;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class Refund : FullAuditedAggregateRoot<Guid>, IMultiTenant, IMultiStore
    {
        public virtual Guid? TenantId { get; protected set; }
        
        public virtual Guid StoreId { get; protected set; }
        
        public virtual Guid OrderId { get; protected set; }
        
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
    }
}