using System;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderLine
    {
        Guid ProductId { get; }
        
        Guid ProductSkuId { get; }
        
        DateTime ProductModificationTime { get; }
        
        DateTime ProductDetailModificationTime { get; }
        
        string ProductTypeName { get; }

        string ProductName { get; }
        
        string SkuDescription { get; }
        
        string MediaResources { get; }
        
        string Currency { get; }
        
        decimal UnitPrice { get; }
        
        decimal TotalPrice { get; }
        
        decimal TotalDiscount { get; }

        int Quantity { get; }
    }
}