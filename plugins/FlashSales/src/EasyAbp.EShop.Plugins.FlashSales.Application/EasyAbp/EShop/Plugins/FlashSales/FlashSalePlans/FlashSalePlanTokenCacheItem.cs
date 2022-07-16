using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

[Serializable]
public class FlashSalePlanPreOrderCacheItem : IMultiTenant
{
    public Guid? TenantId { get; set; }

    public string HashToken { get; set; }

    public Guid PlanId { get; set; }

    public Guid ProductId { get; set; }

    public Guid ProductSkuId { get; set; }

    public string InventoryProviderName { get; set; }
}
