using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSku : FullAuditedEntity<Guid>, IProductSku, IHasExtraProperties
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
        

        public virtual Dictionary<string, object> ExtraProperties { get; protected set; }

        public virtual Guid? ProductDetailId { get; protected set; }
        
        public virtual Dictionary<string, object> ExtraProperties { get; protected set; }

        protected ProductSku()
        {
            ExtraProperties = new Dictionary<string, object>();
            this.SetDefaultsForExtraProperties();
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
            [CanBeNull] string mediaResources,
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
            ProductDetailId = productDetailId;
            
            ExtraProperties = new Dictionary<string, object>();
            this.SetDefaultsForExtraProperties();
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