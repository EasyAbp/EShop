using System;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class FlashSalesOptions
{
    /// <summary>
    /// Default: 3 minutes
    /// </summary>
    public TimeSpan PreOrderExpires { get; set; } = TimeSpan.FromMinutes(3);

    /// <summary>
    /// Default: 5 minutes
    /// </summary>
    public TimeSpan UserFlashSaleResultCacheExpires { get; set; } = TimeSpan.FromMinutes(5);
}
