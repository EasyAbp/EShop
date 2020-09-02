using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Stores.Transactions
{
    public class Transaction : CreationAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        
        public virtual Guid StoreId { get; protected set; }
        
        public virtual Guid? OrderId { get; protected set; }

        public virtual TransactionType TransactionType { get; protected set; }
        
        [NotNull]
        public virtual string ActionName { get; protected set; }
        
        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal Amount { get; protected set; }

        protected Transaction()
        {
        }

        public Transaction(Guid id,
            Guid? tenantId,
            Guid storeId,
            Guid? orderId,
            TransactionType transactionType,
            [NotNull] string actionName,
            [NotNull] string currency,
            decimal amount) : base(id)
        {
            TenantId = tenantId;
            StoreId = storeId;
            OrderId = orderId;
            TransactionType = transactionType;
            ActionName = actionName;
            Currency = currency;
            Amount = amount;
        }
    }
}
