namespace EasyAbp.EShop.Baskets
{
    public static class BasketsDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Baskets";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "Baskets";
    }
}
