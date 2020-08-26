namespace EasyAbp.EShop.Payments
{
    public static class PaymentsDbProperties
    {
        public static string DbTablePrefix { get; set; } = "EasyAbpEShopPayments";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "EasyAbpEShopPayments";
    }
}
