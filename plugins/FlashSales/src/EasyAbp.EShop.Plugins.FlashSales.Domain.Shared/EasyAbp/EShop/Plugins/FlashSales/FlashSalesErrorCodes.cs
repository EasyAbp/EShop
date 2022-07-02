namespace EasyAbp.EShop.Plugins.FlashSales;

public static class FlashSalesErrorCodes
{
    public const string Namespace = "EasyAbp.EShop.Plugins.FlashSales";

    public const string EndTimeMustBeLaterThanBeginTime = $"{Namespace}:{nameof(EndTimeMustBeLaterThanBeginTime)}";

    public const string ProductIsNotInThisStore = $"{Namespace}:{nameof(ProductIsNotInThisStore)}";

    public const string ProductSkuIsNotFound = $"{Namespace}:{nameof(ProductSkuIsNotFound)}";

    public const string ProductIsNotPublished = $"{Namespace}:{nameof(ProductIsNotPublished)}";

    public const string ProductSkuInventoryExceeded = $"{Namespace}:{nameof(ProductSkuInventoryExceeded)}";

    public const string ProductInventoryStrategyIsNotFlashSales = $"{Namespace}:{nameof(ProductInventoryStrategyIsNotFlashSales)}";

    public const string PreOrderExpried = $"{Namespace}:{nameof(PreOrderExpried)}";

    public const string FlashSalesPlanIsNotStart = $"{Namespace}:{nameof(FlashSalesPlanIsNotStart)}";

    public const string FlashSalesPlanIsExpired = $"{Namespace}:{nameof(FlashSalesPlanIsExpired)}";

    public const string CreateFlashSalesOrderBusy = $"{Namespace}:{nameof(CreateFlashSalesOrderBusy)}";

    public const string AlreadySubmitCreateFlashSalesOrder = $"{Namespace}:{nameof(AlreadySubmitCreateFlashSalesOrder)}";
}
