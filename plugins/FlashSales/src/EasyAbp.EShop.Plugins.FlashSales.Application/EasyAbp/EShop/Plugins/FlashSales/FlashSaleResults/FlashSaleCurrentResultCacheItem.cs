using System;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

[Serializable]
public class FlashSaleCurrentResultCacheItem : IMultiTenant
{
    public Guid? TenantId { get; set; }
    
    public FlashSaleResultDto ResultDto { get; set; }
}