namespace EasyAbp.EShop.Orders
{
    public static class OrdersDbProperties
    {
        public static string DbTablePrefix { get; set; } = "EasyAbpEShopOrders";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "EasyAbpEShopOrders";
    }
}
