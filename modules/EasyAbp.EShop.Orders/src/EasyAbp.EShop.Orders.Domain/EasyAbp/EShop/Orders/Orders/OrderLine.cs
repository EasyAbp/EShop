using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderLine : FullAuditedEntity<Guid>, IOrderLine
    {
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
        
        protected OrderLine() {}

        public OrderLine(
            Guid id,
            Guid productId,
            Guid productSkuId,
            DateTime productModificationTime,
            DateTime productDetailModificationTime,
            [NotNull] string productName,
            [CanBeNull] string skuDescription,
            [CanBeNull] string mediaResources,
            [NotNull] string currency,
            decimal unitPrice,
            decimal totalPrice,
            decimal totalDiscount,
            int quantity) : base(id)
        {
            ProductId = productId;
            ProductSkuId = productSkuId;
            ProductModificationTime = productModificationTime;
            ProductDetailModificationTime = productDetailModificationTime;
            ProductName = productName;
            SkuDescription = skuDescription;
            MediaResources = mediaResources;
            Currency = currency;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
            TotalDiscount = totalDiscount;
            Quantity = quantity;
        }
    }
}