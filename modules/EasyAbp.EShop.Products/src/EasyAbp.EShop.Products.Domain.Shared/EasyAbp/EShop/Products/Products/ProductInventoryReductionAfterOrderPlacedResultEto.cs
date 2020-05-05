using System;

namespace EasyAbp.EShop.Products.Products
{
    [Serializable]
    public class ProductInventoryReductionAfterOrderPlacedResultEto
    {
        public Guid OrderId { get; set; }
        
        public bool IsSuccess { get; set; }
    }
}