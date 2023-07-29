using System;
using EasyAbp.EShop.Products.Products;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.DynamicProxy;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderLine : FullAuditedEntity<Guid>, IOrderLine
    {
        public virtual Guid ProductId { get; protected set; }

        public virtual Guid ProductSkuId { get; protected set; }

        public virtual Guid? ProductDetailId { get; protected set; }

        public virtual DateTime ProductModificationTime { get; protected set; }

        public virtual DateTime? ProductDetailModificationTime { get; protected set; }

        public virtual string ProductGroupName { get; protected set; }

        public virtual string ProductGroupDisplayName { get; protected set; }

        public virtual string ProductUniqueName { get; protected set; }

        public virtual string ProductDisplayName { get; protected set; }

        public virtual InventoryStrategy? ProductInventoryStrategy { get; protected set; }

        public virtual string SkuName { get; protected set; }

        public virtual string SkuDescription { get; protected set; }

        public virtual string MediaResources { get; protected set; }

        public virtual string Currency { get; protected set; }

        public virtual decimal UnitPrice { get; protected set; }

        public virtual decimal TotalPrice { get; protected set; }

        public virtual decimal TotalDiscount { get; protected set; }

        public virtual decimal ActualTotalPrice { get; protected set; }

        public virtual int Quantity { get; protected set; }

        public virtual int RefundedQuantity { get; protected set; }

        public virtual decimal RefundAmount { get; protected set; }

        public virtual decimal? PaymentAmount { get; protected set; }

        public ExtraPropertyDictionary ExtraProperties { get; protected set; }

        protected OrderLine()
        {
            ExtraProperties = new ExtraPropertyDictionary();
            this.SetDefaultsForExtraProperties(ProxyHelper.UnProxy(this).GetType());
        }

        public OrderLine(
            Guid id,
            Guid productId,
            Guid productSkuId,
            Guid? productDetailId,
            DateTime productModificationTime,
            DateTime? productDetailModificationTime,
            [NotNull] string productGroupName,
            [NotNull] string productGroupDisplayName,
            [CanBeNull] string productUniqueName,
            [NotNull] string productDisplayName,
            InventoryStrategy productInventoryStrategy,
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
            ProductDetailId = productDetailId;
            ProductModificationTime = productModificationTime;
            ProductDetailModificationTime = productDetailModificationTime;
            ProductGroupName = productGroupName;
            ProductGroupDisplayName = productGroupDisplayName;
            ProductUniqueName = productUniqueName;
            ProductDisplayName = productDisplayName;
            ProductInventoryStrategy = productInventoryStrategy;
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

            ExtraProperties = new ExtraPropertyDictionary();
            this.SetDefaultsForExtraProperties(ProxyHelper.UnProxy(this).GetType());
        }

        internal void Refund(int quantity, decimal amount)
        {
            if (RefundedQuantity + quantity > Quantity)
            {
                throw new InvalidRefundQuantityException(quantity);
            }

            // PaymentAmount is always null before EShop v5
            var paymentAmount = PaymentAmount ?? ActualTotalPrice;
            if (amount <= decimal.Zero || RefundAmount + amount > paymentAmount)
            {
                throw new InvalidRefundAmountException(amount);
            }

            RefundedQuantity += quantity;
            RefundAmount += amount;
        }

        internal void AddDiscount(decimal expectedDiscountAmount)
        {
            TotalDiscount += expectedDiscountAmount;
            ActualTotalPrice -= expectedDiscountAmount;

            if (ActualTotalPrice < decimal.Zero)
            {
                throw new DiscountAmountOverflowException();
            }
        }

        internal void SetPaymentAmount(decimal? paymentAmount)
        {
            PaymentAmount = paymentAmount;
        }
    }
}