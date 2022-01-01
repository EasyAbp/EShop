namespace EasyAbp.EShop.Plugins
{
    public static class PluginsDbProperties
    {
        public static string DbTablePrefix { get; set; } = "EasyAbpEShopPlugins";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "EasyAbpEShopPlugins";
    }
}
