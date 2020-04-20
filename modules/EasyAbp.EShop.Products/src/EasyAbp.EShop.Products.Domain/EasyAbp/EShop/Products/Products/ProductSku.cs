using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSku : FullAuditedEntity<Guid>
    {
        public virtual string SerializedAttributeOptionIds { get; protected set; }
        
        public virtual decimal OriginalPrice { get; protected set; }
        
        public virtual decimal Price { get; protected set; }
        
        public virtual int Inventory { get; protected set; }
        
        public virtual int Sold { get; protected set; }
        
        public virtual int OrderMinQuantity { get; protected set; }
    }
}