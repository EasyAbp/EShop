namespace EasyAbp.EShop
{
    public static class EShopDbProperties
    {
        public static string DbTablePrefix { get; set; } = "EShop";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "EShop";
    }
}
