namespace EasyAbp.EShop.Orders
{
    public static class OrdersErrorCodes
    {
        public const string UnexpectedCurrency = "EasyAbp.EShop.Orders:UnexpectedCurrency";
        public const string OrderLineInvalidQuantity = "EasyAbp.EShop.Orders:OrderLineInvalidQuantity";
        public const string DiscountAmountOverflow = "EasyAbp.EShop.Orders:DiscountAmountOverflow";
        public const string DuplicateOrderExtraFee = "EasyAbp.EShop.Orders:DuplicateOrderExtraFee";
        public const string DuplicateOrderDiscount = "EasyAbp.EShop.Orders:DuplicateOrderDiscount";
        public const string InvalidOrderExtraFee = "EasyAbp.EShop.Orders:InvalidOrderExtraFee";
        public const string InvalidPayment = "EasyAbp.EShop.Orders:InvalidPayment";
        public const string InvalidRefundAmount = "EasyAbp.EShop.Orders:InvalidRefundAmount";
        public const string InvalidRefundQuantity = "EasyAbp.EShop.Orders:InvalidRefundQuantity";
        public const string OrderIsInWrongStage = "EasyAbp.EShop.Orders:OrderIsInWrongStage";
        public const string ExistFlashSalesProduct = "EasyAbp.EShop.Orders:ExistFlashSalesProduct";
        public const string OrderLinesShouldNotBeEmpty = "EasyAbp.EShop.Orders:OrderLinesShouldNotBeEmpty";
        public const string QuantityShouldBeGreaterThanZero = "EasyAbp.EShop.Orders:QuantityShouldBeGreaterThanZero";
    }
}
