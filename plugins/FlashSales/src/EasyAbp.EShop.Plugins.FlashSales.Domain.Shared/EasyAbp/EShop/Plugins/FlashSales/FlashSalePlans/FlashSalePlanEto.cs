using System;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

[Serializable]
public class FlashSalePlanEto : FullAuditedEntityEto<Guid>, IMultiTenant
{
    public Guid? TenantId { get; set; }

    public Guid StoreId { get; set; }

    public DateTime BeginTime { get; set; }

    public DateTime EndTime { get; set; }

    public Guid ProductId { get; set; }

    public Guid ProductSkuId { get; set; }

    public bool IsPublished { get; set; }
}