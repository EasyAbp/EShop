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
        public virtual string Code { get; protected set; }
        
        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal? OriginalPrice { get; protected set; }
        
        public virtual decimal Price { get; protected set; }

        public virtual int OrderMinQuantity { get; protected set; }
        
        public virtual int OrderMaxQuantity { get; protected set; }
        
        [CanBeNull]
        public virtual string MediaResources { get; protected set; }
        
        public Guid? ProductDetailId { get; set; }

        protected ProductSku() {}
        
        public ProductSku(
            Guid id,
            [NotNull] string serializedAttributeOptionIds,
            [CanBeNull] string code,
            [NotNull] string currency,
            decimal? originalPrice,
            decimal price,
            int orderMinQuantity,
            int orderMaxQuantity,
            [CanBeNull] string mediaResources,
            Guid? productDetailId) : base(id)
        {
            SerializedAttributeOptionIds = serializedAttributeOptionIds;
            Code = code?.Trim();
            Currency = currency;
            OriginalPrice = originalPrice;
            Price = price;
            OrderMinQuantity = orderMinQuantity;
            OrderMaxQuantity = orderMaxQuantity;
            MediaResources = mediaResources;
            ProductDetailId = productDetailId;
        }

        public void TrimCode()
        {
            Code = Code?.Trim();
        }

        public void SetSerializedAttributeOptionIds(string serializedAttributeOptionIds)
        {
            SerializedAttributeOptionIds = serializedAttributeOptionIds;
        }
    }
}