using System;
using JetBrains.Annotations;
using NodaMoney;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.DynamicProxy;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSku : FullAuditedEntity<Guid>, IProductSku
    {
        [NotNull]
        public virtual string SerializedAttributeOptionIds { get; protected set; }

        [CanBeNull]
        public virtual string Name { get; protected set; }

        [CanBeNull]
        public virtual string Description { get; protected set; }

        [NotNull]
        public virtual string Currency { get; protected set; }

        public virtual decimal? OriginalPrice { get; protected set; }

        public virtual decimal Price { get; protected set; }

        public virtual int OrderMinQuantity { get; protected set; }

        public virtual int OrderMaxQuantity { get; protected set; }

        public virtual TimeSpan? PaymentExpireIn { get; protected set; }

        [CanBeNull]
        public virtual string MediaResources { get; protected set; }

        public virtual Guid? ProductDetailId { get; protected set; }

        public ExtraPropertyDictionary ExtraProperties { get; protected set; }

        protected ProductSku()
        {
            ExtraProperties = new ExtraPropertyDictionary();
            this.SetDefaultsForExtraProperties(ProxyHelper.UnProxy(this).GetType());
        }

        public ProductSku(
            Guid id,
            [NotNull] string serializedAttributeOptionIds,
            [CanBeNull] string name,
            [NotNull] string currency,
            decimal? originalPrice,
            decimal price,
            int orderMinQuantity,
            int orderMaxQuantity,
            TimeSpan? paymentExpireIn,
            [CanBeNull] string mediaResources,
            Guid? productDetailId) : base(id)
        {
            Check.NotNullOrWhiteSpace(currency, nameof(currency));
            var nodaCurrency = NodaMoney.Currency.FromCode(currency);

            SerializedAttributeOptionIds =
                Check.NotNullOrWhiteSpace(serializedAttributeOptionIds, nameof(serializedAttributeOptionIds));
            Name = name?.Trim();
            Currency = nodaCurrency.Code;
            OriginalPrice = originalPrice.HasValue ? new Money(originalPrice.Value, nodaCurrency).Amount : null;
            Price = new Money(price, nodaCurrency).Amount;
            OrderMinQuantity = orderMinQuantity;
            OrderMaxQuantity = orderMaxQuantity;
            PaymentExpireIn = paymentExpireIn;
            MediaResources = mediaResources;
            ProductDetailId = productDetailId;

            ExtraProperties = new ExtraPropertyDictionary();
            this.SetDefaultsForExtraProperties(ProxyHelper.UnProxy(this).GetType());
        }

        internal void TrimName()
        {
            Name = Name?.Trim();
        }

        public void Update(
            [CanBeNull] string name,
            [NotNull] string currency,
            decimal? originalPrice,
            decimal price,
            int orderMinQuantity,
            int orderMaxQuantity,
            TimeSpan? paymentExpireIn,
            [CanBeNull] string mediaResources,
            Guid? productDetailId)
        {
            Check.NotNullOrWhiteSpace(currency, nameof(currency));
            var nodaCurrency = NodaMoney.Currency.FromCode(currency);

            Name = name?.Trim();
            Currency = nodaCurrency.Code;
            OriginalPrice = originalPrice.HasValue ? new Money(originalPrice.Value, nodaCurrency).Amount : null;
            Price = new Money(price, nodaCurrency).Amount;
            OrderMinQuantity = orderMinQuantity;
            OrderMaxQuantity = orderMaxQuantity;
            PaymentExpireIn = paymentExpireIn;
            MediaResources = mediaResources;
            ProductDetailId = productDetailId;
        }
    }
}