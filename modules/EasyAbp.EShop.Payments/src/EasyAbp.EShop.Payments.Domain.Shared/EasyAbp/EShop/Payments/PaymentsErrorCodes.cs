namespace EasyAbp.EShop.Payments
{
    public static class PaymentsErrorCodes
    {
        public const string MultiStorePaymentNotSupported = "EasyAbp.EShop.Payments:MultiStorePaymentNotSupported";
        public const string InvalidRefundQuantity = "EasyAbp.EShop.Payments:InvalidRefundQuantity";
        public const string OrderIsNotInSpecifiedPayment = "EasyAbp.EShop.Payments:OrderIsNotInSpecifiedPayment";
        public const string AnotherRefundTaskIsOnGoing = "EasyAbp.EShop.Payments:AnotherRefundTaskIsOnGoing";
        public const string InvalidRefundAmount = "EasyAbp.EShop.Payments:InvalidRefundAmount";
        public const string OrderIdNotFound = "EasyAbp.EShop.Payments:OrderIdNotFound";
        public const string StoreIdNotFound = "EasyAbp.EShop.Payments:StoreIdNotFound";
        public const string OrderLineNotFound = "EasyAbp.EShop.Payments:OrderLineNotFound";
        public const string OrderExtraFeeNotFound = "EasyAbp.EShop.Payments:OrderExtraFeeNotFound";
    }
}
