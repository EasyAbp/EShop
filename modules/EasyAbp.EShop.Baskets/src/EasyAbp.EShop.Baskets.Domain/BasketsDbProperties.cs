namespace EasyAbp.EShop.Baskets
{
    public static class BasketsDbProperties
    {
        public static string DbTablePrefix { get; set; } = "EShopBaskets";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "EShopBaskets";
    }
}
