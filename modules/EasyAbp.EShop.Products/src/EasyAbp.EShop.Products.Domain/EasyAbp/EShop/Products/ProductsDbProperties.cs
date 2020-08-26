namespace EasyAbp.EShop.Products
{
    public static class ProductsDbProperties
    {
        public static string DbTablePrefix { get; set; } = "EasyAbpEShopProducts";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "EasyAbpEShopProducts";
    }
}
