using System;

namespace EasyAbp.EShop.Products.Products
{
    public class ConsumeInventoryModel
    {
        public Product Product { get; }
        
        public ProductSku ProductSku { get; }
        
        public Guid StoreId { get; }
        
        public int Quantity { get; }
        
        public bool Consumed { get; private set;}

        public ConsumeInventoryModel(Product product, ProductSku productSku, Guid storeId, int quantity)
        {
            Product = product;
            ProductSku = productSku;
            StoreId = storeId;
            Quantity = quantity;
        }

        public void SetConsumed(bool consumed)
        {
            Consumed = consumed;
        }
    }
}