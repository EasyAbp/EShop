using System;

namespace EasyAbp.EShop.Products.Products
{
    public class ReduceInventoryModel
    {
        public Product Product { get; set; }
        
        public ProductSku ProductSku { get; set; }
        
        public Guid StoreId { get; set; }
        
        public int Quantity { get; set; }
    }
}