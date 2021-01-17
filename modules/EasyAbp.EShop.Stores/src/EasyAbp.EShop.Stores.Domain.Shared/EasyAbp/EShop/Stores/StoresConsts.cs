namespace EasyAbp.EShop.Stores
{
    public static class StoresConsts
    {
        public const string TransactionOrderCompletedActionName = "OrderCompleted";
        
        public const string TransactionOrderRefundedActionName = "OrderRefunded";
        
        public const string StoreRouteBase = "/api/e-shop/stores/store";

        public const string GetStoreListedDataSourceUrl = StoreRouteBase;
        
        public const string GetStoreSingleDataSourceUrl = StoreRouteBase + "/{id}";
    }
}