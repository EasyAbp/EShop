using System;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

[Serializable]
public class FlashSalePlanCacheItem : FlashSalePlanDto
{
    public Guid? TenantId { get; set; }
}
