using System;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

[Serializable]
public class FlashSalePlanCacheItem : FlashSalePlanDto, IMultiTenant
{
    public Guid? TenantId { get; set; }
}
