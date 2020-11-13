namespace EasyAbp.EShop.Products
{
    public static class ProductsConsts
    {
        public const string DefaultProductGroupName = "Default";
        
        public const string DefaultProductGroupDisplayName = "Default";
        
        public const string DefaultProductGroupDescription = "";

        public const string CategoryRouteBase = "/api/eShop/products/category";
        
        public const string GetCategorySummaryListedDataSourceUrl = CategoryRouteBase + "/summary";
        
        public const string GetCategorySummarySingleDataSourceUrl = CategoryRouteBase + "/{id}";
    }
}