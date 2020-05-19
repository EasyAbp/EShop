namespace EasyAbp.EShop.Products
{
    public static class ProductsDbProperties
    {
        public static string DbTablePrefix { get; set; } = "EShopProducts";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "EShopProducts";
    }
}
