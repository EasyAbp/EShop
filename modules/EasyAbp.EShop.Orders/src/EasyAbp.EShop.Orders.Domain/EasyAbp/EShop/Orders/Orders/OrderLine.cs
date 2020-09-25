using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Data;
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
        public virtual string ProductGroupName { get; protected set; }
        
        [NotNull]
        public virtual string ProductGroupDisplayName { get; protected set; }
        
        [CanBeNull]
        public virtual string ProductUniqueName { get; protected set; }
        
        [NotNull]
        public virtual string ProductDisplayName { get; protected set; }
                
        [CanBeNull]
        public virtual string SkuName { get; protected set; }
        
        [CanBeNull]
        public virtual string SkuDescription { get; protected set; }
        
        [CanBeNull]
        public virtual string MediaResources { get; protected set; }
        
        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal UnitPrice { get; protected set; }
        
        public virtual decimal TotalPrice { get; protected set; }
        
        public virtual decimal TotalDiscount { get; protected set; }
        
        public virtual decimal ActualTotalPrice { get; protected set; }

        public virtual int Quantity { get; protected set; }
        
        public virtual int RefundedQuantity { get; protected set; }
        
        public virtual decimal RefundAmount { get; protected set; }
        
        public virtual Dictionary<string, object> ExtraProperties { get; protected set; }

        protected OrderLine()
        {
            ExtraProperties = new Dictionary<string, object>();
            this.SetDefaultsForExtraProperties();
        }

        public OrderLine(
            Guid id,
            Guid productId,
            Guid productSkuId,
            DateTime productModificationTime,
            DateTime productDetailModificationTime,
            [NotNull] string productGroupName,
            [NotNull] string productGroupDisplayName,
            [CanBeNull] string productUniqueName,
            [NotNull] string productDisplayName,
            [CanBeNull] string skuName,
            [CanBeNull] string skuDescription,
            [CanBeNull] string mediaResources,
            [NotNull] string currency,
            decimal unitPrice,
            decimal totalPrice,
            decimal totalDiscount,
            decimal actualTotalPrice,
            int quantity) : base(id)
        {
            ProductId = productId;
            ProductSkuId = productSkuId;
            ProductModificationTime = productModificationTime;
            ProductDetailModificationTime = productDetailModificationTime;
            ProductGroupName = productGroupName;
            ProductGroupDisplayName = productGroupDisplayName;
            ProductUniqueName = productUniqueName;
            ProductDisplayName = productDisplayName;
            SkuName = skuName;
            SkuDescription = skuDescription;
            MediaResources = mediaResources;
            Currency = currency;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
            TotalDiscount = totalDiscount;
            ActualTotalPrice = actualTotalPrice;
            Quantity = quantity;

            RefundedQuantity = 0;
            RefundAmount = 0;
            
            ExtraProperties = new Dictionary<string, object>();
            this.SetDefaultsForExtraProperties();
        }

        internal void Refund(int quantity, decimal amount)
        {
            RefundedQuantity += quantity;
            RefundAmount += amount;
        }
        
        internal decimal AddDiscountGetRealDiscountedAmount(decimal expectedDiscountAmount)
        {
            var realDiscountedAmount = ActualTotalPrice - expectedDiscountAmount >= decimal.Zero
                ? ActualTotalPrice - expectedDiscountAmount
                : ActualTotalPrice;
            
            TotalDiscount += realDiscountedAmount;
            ActualTotalPrice -= realDiscountedAmount;

            return realDiscountedAmount;
        }
    }
}