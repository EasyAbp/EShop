using System;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductSku : IHasExtraProperties
    {
        string SerializedAttributeOptionIds { get; }
        
        string Name { get; }
        
        string Currency { get; }
        
        decimal? OriginalPrice { get; }
        
        decimal Price { get; }

        int OrderMinQuantity { get; }
        
        int OrderMaxQuantity { get; }
        
        string MediaResources { get; }
        
        Guid? ProductDetailId { get; }
    }
}