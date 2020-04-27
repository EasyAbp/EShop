using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSku : FullAuditedEntity<Guid>
    {
        [NotNull]
        public virtual string SerializedAttributeOptionIds { get; protected set; }
        
        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal OriginalPrice { get; protected set; }
        
        public virtual decimal Price { get; protected set; }

        public virtual int Inventory { get; protected set; }
        
        public virtual int Sold { get; protected set; }
        
        public virtual int OrderMinQuantity { get; protected set; }

        protected ProductSku() {}
        
        public ProductSku(
            Guid id,
            [NotNull] string serializedAttributeOptionIds,
            [NotNull] string currency,
            decimal originalPrice,
            decimal price,
            int inventory,
            int sold,
            int orderMinQuantity) : base(id)
        {
            SerializedAttributeOptionIds = serializedAttributeOptionIds;
            Currency = currency;
            OriginalPrice = originalPrice;
            Price = price;
            Inventory = inventory;
            Sold = sold;
            OrderMinQuantity = orderMinQuantity;
        }
    }
}