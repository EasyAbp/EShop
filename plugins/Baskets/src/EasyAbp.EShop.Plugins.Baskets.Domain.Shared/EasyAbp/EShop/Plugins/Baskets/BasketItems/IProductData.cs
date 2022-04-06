namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public interface IProductData
    {
        string MediaResources { get; }
        
        string ProductUniqueName { get; }
        
        string ProductDisplayName { get; }
        
        string SkuName { get; }
        
        string SkuDescription { get; }

        string Currency { get; }
        
        decimal UnitPrice { get; }
        
        decimal TotalPrice { get; }
        
        decimal TotalDiscount { get; }
        
        int Inventory { get; }
    }
}