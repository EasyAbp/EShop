namespace EasyAbp.EShop.Inventory
{
    public static class InventoryDbProperties
    {
        public static string DbTablePrefix { get; set; } = "EasyAbpEShopInventory";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "EasyAbpEShopInventory";
    }
}
