using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

[Serializable]
public class FlashSaleProductDetailEto : FullAuditedEntityEto<Guid>, IMultiTenant
{
    public Guid? TenantId { get; set; }

    public Guid? StoreId { get; set; }

    public string Description { get; set; }
}