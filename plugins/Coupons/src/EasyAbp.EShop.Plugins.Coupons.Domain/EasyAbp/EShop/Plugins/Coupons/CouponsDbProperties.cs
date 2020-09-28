namespace EasyAbp.EShop.Plugins.Coupons
{
    public static class CouponsDbProperties
    {
        public static string DbTablePrefix { get; set; } = "EasyAbpEShopPluginsCoupons";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "EasyAbpEShopPluginsCoupons";
    }
}
