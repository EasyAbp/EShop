using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public class ProductInventory : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid ProductId { get; protected set; }
        
        public virtual Guid ProductSkuId { get; protected set; }
        
        public virtual int Inventory { get; protected set; }
        
        public virtual long Sold { get; protected set; }

        protected ProductInventory()
        {
        }

        public ProductInventory(
            Guid id,
            Guid productId,
            Guid productSkuId,
            int inventory,
            long sold) : base(id)
        {
            ProductId = productId;
            ProductSkuId = productSkuId;
            Inventory = inventory;
            Sold = sold;
        }
        
        internal bool TryIncreaseInventory(int quantity, bool decreaseSold)
        {
            if (quantity < 0)
            {
                return false;
            }

            if (decreaseSold && Sold - quantity < 0)
            {
                return false;
            }
            
            Inventory = checked(Inventory + quantity);

            if (decreaseSold)
            {
                Sold -= quantity;
            }

            return true;
        }

        internal bool TryReduceInventory(int quantity, bool increaseSold)
        {
            if (quantity > Inventory || quantity < 0)
            {
                return false;
            }

            Inventory -= quantity;

            if (increaseSold)
            {
                Sold = checked(Sold + quantity);
            }
            
            return true;
        }
    }
}
