using System;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

[Serializable]
public class FlashSalePlanCacheItem : FlashSalePlanDto
{
    public Guid? TenantId { get; set; }
}
