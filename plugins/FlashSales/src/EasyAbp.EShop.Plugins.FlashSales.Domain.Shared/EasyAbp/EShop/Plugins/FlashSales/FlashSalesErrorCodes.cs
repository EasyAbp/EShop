namespace EasyAbp.EShop.Plugins.FlashSales;

public static class FlashSalesErrorCodes
{
    public const string Namespace = "EasyAbp.EShop.Plugins.FlashSales";

    public const string EndTimeMustBeLaterThanBeginTime = $"{Namespace}:{nameof(EndTimeMustBeLaterThanBeginTime)}";

}
