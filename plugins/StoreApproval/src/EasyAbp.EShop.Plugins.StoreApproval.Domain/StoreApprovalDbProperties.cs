namespace EasyAbp.EShop.Plugins.StoreApproval
{
    public static class StoreApprovalDbProperties
    {
        public static string DbTablePrefix { get; set; } = "StoreApproval";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "StoreApproval";
    }
}
