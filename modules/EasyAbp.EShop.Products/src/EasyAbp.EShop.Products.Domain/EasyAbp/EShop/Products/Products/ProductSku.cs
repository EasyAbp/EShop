using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSku : FullAuditedEntity<Guid>, IProductSku
    {
        [NotNull]
        public virtual string SerializedAttributeOptionIds { get; protected set; }
        
        [CanBeNull]
        public virtual string Name { get; protected set; }
        
        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal? OriginalPrice { get; protected set; }
        
        public virtual decimal Price { get; protected set; }

        public virtual int OrderMinQuantity { get; protected set; }
        
        public virtual int OrderMaxQuantity { get; protected set; }
        
        [CanBeNull]
        public virtual string MediaResources { get; protected set; }
        
        [CanBeNull]
        public virtual string SpecifiedInventoryProviderName { get; protected set; }
        
        public Guid? ProductDetailId { get; set; }

        protected ProductSku() {}
        
        public ProductSku(
            Guid id,
            [NotNull] string serializedAttributeOptionIds,
            [CanBeNull] string name,
            [NotNull] string currency,
            decimal? originalPrice,
            decimal price,
            int orderMinQuantity,
            int orderMaxQuantity,
            [CanBeNull] string mediaResources,
            [CanBeNull] string specifiedInventoryProviderName,
            Guid? productDetailId) : base(id)
        {
            SerializedAttributeOptionIds = serializedAttributeOptionIds;
            Name = name?.Trim();
            Currency = currency;
            OriginalPrice = originalPrice;
            Price = price;
            OrderMinQuantity = orderMinQuantity;
            OrderMaxQuantity = orderMaxQuantity;
            MediaResources = mediaResources;
            SpecifiedInventoryProviderName = specifiedInventoryProviderName;
            ProductDetailId = productDetailId;
        }

        public void TrimCode()
        {
            Name = Name?.Trim();
        }

        public void SetSerializedAttributeOptionIds(string serializedAttributeOptionIds)
        {
            SerializedAttributeOptionIds = serializedAttributeOptionIds;
        }
    }
}