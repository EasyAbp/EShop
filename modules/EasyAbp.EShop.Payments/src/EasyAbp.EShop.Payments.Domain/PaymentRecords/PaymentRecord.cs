using System;
using System.Collections.Generic;
using EasyAbp.EShop.Stores.Stores;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Payments.PaymentsRecords
{
    public class PaymentsRecord : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        
        [NotNull]
        public virtual string PaymentsMethod { get; protected set; }
        
        [NotNull]
        public virtual string ExternalTradingCode { get; protected set; }
        
        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal OriginalPaymentsAmount { get; protected set; }

        public virtual decimal PaymentsDiscount { get; protected set; }
        
        public virtual decimal ActualPaymentsAmount { get; protected set; }
        
        public virtual decimal RefundAmount { get; protected set; }
        
        public virtual List<PaymentsRecordOrder> PaymentsRecordOrders { get; protected set; }
    }
}