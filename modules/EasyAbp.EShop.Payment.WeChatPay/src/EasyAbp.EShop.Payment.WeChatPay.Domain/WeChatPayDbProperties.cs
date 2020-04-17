namespace EasyAbp.EShop.Payment.WeChatPay
{
    public static class WeChatPayDbProperties
    {
        public static string DbTablePrefix { get; set; } = "WeChatPay";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "WeChatPay";
    }
}
