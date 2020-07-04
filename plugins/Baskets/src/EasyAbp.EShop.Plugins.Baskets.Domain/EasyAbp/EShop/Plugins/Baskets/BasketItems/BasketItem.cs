using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public class BasketItem : CreationAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        
        [NotNull]
        public virtual string BasketName { get; protected set; }
        
        public virtual Guid UserId { get; protected set; }
        
        public virtual Guid ProductId { get; protected set; }
        
        public virtual Guid ProductSkuId { get; protected set; }
        
        public virtual int Quantity { get; protected set; }
        
        [CanBeNull]
        public virtual string MediaResources { get; protected set; }
        
        [NotNull]
        public virtual string ProductName { get; protected set; }
        
        [NotNull]
        public virtual string SkuDescription { get; protected set; }

        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal UnitPrice { get; protected set; }
        
        public virtual decimal TotalPrice { get; protected set; }
        
        public virtual decimal TotalDiscount { get; protected set; }
    }
}