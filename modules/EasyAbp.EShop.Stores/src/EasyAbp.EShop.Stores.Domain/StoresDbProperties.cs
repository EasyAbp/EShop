namespace EasyAbp.EShop.Stores
{
    public static class StoresDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Stores";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "Stores";
    }
}
