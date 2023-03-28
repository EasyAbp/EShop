namespace EasyAbp.EShop.Plugins.Promotions;

public static class PromotionsDbProperties
{
    public static string DbTablePrefix { get; set; } = "EasyAbpEShopPluginsPromotions";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "EasyAbpEShopPluginsPromotions";
}
