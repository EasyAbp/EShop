using System;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

[Serializable]
public class FlashSalePlanPreOrderCacheItem
{
    public string HashToken { get; set; }

    public Guid PlanId { get; set; }

    public Guid ProductId { get; set; }

    public Guid ProductSkuId { get; set; }

    public string InventoryProviderName { get; set; }
}
