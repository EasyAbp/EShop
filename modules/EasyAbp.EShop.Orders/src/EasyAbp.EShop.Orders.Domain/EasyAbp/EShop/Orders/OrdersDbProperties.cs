namespace EasyAbp.EShop.Orders
{
    public static class OrdersDbProperties
    {
        public static string DbTablePrefix { get; set; } = "EShopOrders";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "EShopOrders";
    }
}
