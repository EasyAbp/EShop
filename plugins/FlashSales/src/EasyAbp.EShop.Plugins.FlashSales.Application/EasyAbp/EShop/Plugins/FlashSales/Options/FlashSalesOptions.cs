using System;

namespace EasyAbp.EShop.Plugins.FlashSales.Options;

public class FlashSalesOptions
{
    /// <summary>
    /// Default: 3 minutes
    /// </summary>
    public TimeSpan PreOrderExpires { get; set; } = TimeSpan.FromMinutes(3);

    /// <summary>
    /// Default: 5 minutes
    /// </summary>
    public TimeSpan FlashSaleCurrentResultCacheExpires { get; set; } = TimeSpan.FromMinutes(5);
}
