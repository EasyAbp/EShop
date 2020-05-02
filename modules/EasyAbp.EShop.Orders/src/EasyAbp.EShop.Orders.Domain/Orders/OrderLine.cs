using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderLine : FullAuditedEntity<Guid>
    {
        public virtual Guid OrderId { get; protected set; }
        
        public virtual Guid ProductId { get; protected set; }
        
        public virtual Guid ProductSkuId { get; protected set; }
        
        public virtual DateTime ProductModificationTime { get; protected set; }
        
        public virtual DateTime ProductDetailModificationTime { get; protected set; }
        
        [NotNull]
        public virtual string ProductName { get; protected set; }
        
        [CanBeNull]
        public virtual string SkuDescription { get; protected set; }
        
        [CanBeNull]
        public virtual string MediaResources { get; protected set; }
        
        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal UnitPrice { get; protected set; }
        
        public virtual decimal TotalPrice { get; protected set; }
        
        public virtual decimal TotalDiscount { get; protected set; }

        public virtual int Quantity { get; protected set; }
    }
}