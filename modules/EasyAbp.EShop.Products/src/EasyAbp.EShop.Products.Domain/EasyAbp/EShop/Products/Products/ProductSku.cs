using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSku : FullAuditedEntity<Guid>
    {
        [NotNull]
        public virtual string SerializedAttributeOptionIds { get; protected set; }
        
        [CanBeNull]
        public virtual string Code { get; protected set; }
        
        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal? OriginalPrice { get; protected set; }
        
        public virtual decimal Price { get; protected set; }

        public virtual int Inventory { get; protected set; }
        
        // Todo: should be implemented
        public virtual int Sold { get; protected set; }
        
        public virtual int OrderMinQuantity { get; protected set; }
        
        public virtual int OrderMaxQuantity { get; protected set; }
        
        public Guid? ProductDetailId { get; set; }

        protected ProductSku() {}
        
        public ProductSku(
            Guid id,
            [NotNull] string serializedAttributeOptionIds,
            [CanBeNull] string code,
            [NotNull] string currency,
            decimal? originalPrice,
            decimal price,
            int inventory,
            int sold,
            int orderMinQuantity,
            int orderMaxQuantity,
            Guid? productDetailId) : base(id)
        {
            SerializedAttributeOptionIds = serializedAttributeOptionIds;
            Code = code?.Trim();
            Currency = currency;
            OriginalPrice = originalPrice;
            Price = price;
            Inventory = inventory;
            Sold = sold;
            OrderMinQuantity = orderMinQuantity;
            OrderMaxQuantity = orderMaxQuantity;
            ProductDetailId = productDetailId;
        }

        public bool TryIncreaseInventory(int quantity)
        {
            if (quantity <= 0)
            {
                return false;
            }
            
            Inventory = checked(Inventory + quantity);

            return true;
        }

        public bool TryReduceInventory(int quantity)
        {
            if (quantity > Inventory || quantity <= 0)
            {
                return false;
            }

            Inventory -= quantity;
            
            return true;
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