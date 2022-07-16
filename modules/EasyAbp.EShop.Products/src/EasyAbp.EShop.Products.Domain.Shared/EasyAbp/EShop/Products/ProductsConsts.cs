namespace EasyAbp.EShop.Products
{
    public static class ProductsConsts
    {
        public const string DefaultProductGroupName = "Default";
        
        public const string DefaultProductGroupDisplayName = "Default";
        
        public const string DefaultProductGroupDescription = "";

        public const string RouteBase = "/api/e-shop/products";
        
        public const string GetCategorySummaryListedDataSourceUrl = RouteBase + "/category/summary";
        
        public const string GetCategorySummarySingleDataSourceUrl = RouteBase + "/category/{id}";
        
        public const string GetProductListedDataSourceUrl = RouteBase + "/product";
        
        public const string GetProductSingleDataSourceUrl = RouteBase + "/product/{id}";
        
        public const string DefaultPaymentExpireInSettingName = "EasyAbp.EShop.Products.Product.DefaultPaymentExpireIn";
    }
}