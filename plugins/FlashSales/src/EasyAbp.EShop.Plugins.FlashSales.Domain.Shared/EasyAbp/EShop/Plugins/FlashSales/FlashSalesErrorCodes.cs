namespace EasyAbp.EShop.Plugins.FlashSales;

public static class FlashSalesErrorCodes
{
    public const string Namespace = "EasyAbp.EShop.Plugins.FlashSales";

    public const string InvalidEndTime = $"{Namespace}:{nameof(InvalidEndTime)}";

    public const string ProductIsNotInThisStore = $"{Namespace}:{nameof(ProductIsNotInThisStore)}";

    public const string ProductSkuIsNotFound = $"{Namespace}:{nameof(ProductSkuIsNotFound)}";

    public const string ProductIsNotPublished = $"{Namespace}:{nameof(ProductIsNotPublished)}";

    public const string ProductSkuInventoryExceeded = $"{Namespace}:{nameof(ProductSkuInventoryExceeded)}";

    public const string UnexpectedInventoryStrategy = $"{Namespace}:{nameof(UnexpectedInventoryStrategy)}";

    public const string PreOrderExpired = $"{Namespace}:{nameof(PreOrderExpired)}";

    public const string FlashSaleNotStarted = $"{Namespace}:{nameof(FlashSaleNotStarted)}";

    public const string FlashSaleIsOver = $"{Namespace}:{nameof(FlashSaleIsOver)}";

    public const string BusyToCreateFlashSaleOrder = $"{Namespace}:{nameof(BusyToCreateFlashSaleOrder)}";

    public const string DuplicateFlashSalesOrder = $"{Namespace}:{nameof(DuplicateFlashSalesOrder)}";

    public const string RelatedFlashSaleResultsExist = $"{Namespace}:{nameof(RelatedFlashSaleResultsExist)}";

    public const string FlashSaleResultStatusNotPending = $"{Namespace}:{nameof(FlashSaleResultStatusNotPending)}";
}
