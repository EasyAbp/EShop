namespace EasyAbp.EShop.Plugins.FlashSales;

public static class FlashSalesErrorCodes
{
    public const string Namespace = "EasyAbp.EShop.Plugins.FlashSales";

    public const string EndTimeMustBeLaterThanBeginTime = $"{Namespace}:{nameof(EndTimeMustBeLaterThanBeginTime)}";

    public const string ProductIsNotPublished = $"{Namespace}:{nameof(ProductIsNotPublished)}";

    public const string IsNotFlashSalesProduct = $"{Namespace}:{nameof(IsNotFlashSalesProduct)}";

    public const string PreOrderExipred = $"{Namespace}:{nameof(PreOrderExipred)}";

    public const string FlashSalesPlanIsNotStart = $"{Namespace}:{nameof(FlashSalesPlanIsNotStart)}";

    public const string FlashSalesPlanIsExpired = $"{Namespace}:{nameof(FlashSalesPlanIsExpired)}";

    public const string CreateFlashSalesOrderBusy = $"{Namespace}:{nameof(CreateFlashSalesOrderBusy)}";

    public const string AlreadySubmitCreateFlashSalesOrder = $"{Namespace}:{nameof(AlreadySubmitCreateFlashSalesOrder)}";
}
