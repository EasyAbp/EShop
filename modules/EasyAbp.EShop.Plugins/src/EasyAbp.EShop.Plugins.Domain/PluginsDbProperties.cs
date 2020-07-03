namespace EasyAbp.EShop.Plugins
{
    public static class PluginsDbProperties
    {
        public static string DbTablePrefix { get; set; } = "EShopPlugins";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "EShopPlugins";
    }
}
