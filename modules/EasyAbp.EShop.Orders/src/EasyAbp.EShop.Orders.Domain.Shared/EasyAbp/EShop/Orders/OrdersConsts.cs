namespace EasyAbp.EShop.Orders
{
    public static class OrdersConsts
    {
        public static string UnpaidAutoCancellationReason = "Order payment timed out and not paid";
        public static string InventoryReductionFailedAutoCancellationReason = "Insufficient inventory";
    }
}