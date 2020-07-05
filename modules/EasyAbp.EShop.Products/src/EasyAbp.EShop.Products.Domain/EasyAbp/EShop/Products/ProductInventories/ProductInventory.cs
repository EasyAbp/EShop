using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public class ProductInventory : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid ProductId { get; protected set; }
        
        public virtual Guid ProductSkuId { get; protected set; }
        
        public virtual int Inventory { get; protected set; }
        
        // Todo: should be implemented
        public virtual int Sold { get; protected set; }

        protected ProductInventory()
        {
        }

        public ProductInventory(
            Guid id,
            Guid productId,
            Guid productSkuId,
            int inventory,
            int sold) : base(id)
        {
            ProductId = productId;
            ProductSkuId = productSkuId;
            Inventory = inventory;
            Sold = sold;
        }
        
        internal bool TryIncreaseInventory(int quantity)
        {
            if (quantity < 0)
            {
                return false;
            }
            
            Inventory = checked(Inventory + quantity);

            return true;
        }

        internal bool TryReduceInventory(int quantity)
        {
            if (quantity > Inventory || quantity < 0)
            {
                return false;
            }

            Inventory -= quantity;
            
            return true;
        }
    }
}
