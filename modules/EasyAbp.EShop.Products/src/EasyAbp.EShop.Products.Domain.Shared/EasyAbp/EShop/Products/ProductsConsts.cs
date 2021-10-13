namespace EasyAbp.EShop.Products
{
    public static class ProductsConsts
    {
        public const string DefaultProductGroupName = "Default";
        
        public const string DefaultProductGroupDisplayName = "Default";
        
        public const string DefaultProductGroupDescription = "";

        public const string CategoryRouteBase = "/api/e-shop/products/category";
        
        public const string GetCategorySummaryListedDataSourceUrl = CategoryRouteBase + "/summary";
        
        public const string GetCategorySummarySingleDataSourceUrl = CategoryRouteBase + "/{id}";
        
        public const string DefaultPaymentExpireInSettingName = "EasyAbp.EShop.Products.Product.DefaultPaymentExpireIn";
    }
}