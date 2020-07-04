namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public class ProductDataModel : IProductData
    {
        public string MediaResources { get; set; }
        
        public string ProductName { get; set; }
        
        public string SkuDescription { get; set; }
        
        public string Currency { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public decimal TotalPrice { get; set; }
        
        public decimal TotalDiscount { get; set; }
        
        public int Inventory { get; set; }
    }
}