using System;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductSku
    {
        string SerializedAttributeOptionIds { get; }
        
        string Code { get; }
        
        string Currency { get; }
        
        decimal? OriginalPrice { get; }
        
        decimal Price { get; }

        int Inventory { get; }
        
        int Sold { get; }
        
        int OrderMinQuantity { get; }
        
        int OrderMaxQuantity { get; }
        
        string MediaResources { get; }
        
        public Guid? ProductDetailId { get; }
    }
}