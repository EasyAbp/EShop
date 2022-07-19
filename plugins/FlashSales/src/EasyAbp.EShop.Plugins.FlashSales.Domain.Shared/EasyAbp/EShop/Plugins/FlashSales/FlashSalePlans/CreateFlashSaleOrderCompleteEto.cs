using System;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class CreateFlashSaleOrderCompleteEto : ExtensibleObject, IMultiTenant
{
    public Guid? TenantId { get; set; }

    public Guid PendingResultId { get; set; }

    public bool Success { get; set; }

    public Guid StoreId { get; set; }

    public Guid PlanId { get; set; }

    public string Reason { get; set; }

    public Guid UserId { get; set; }

    public Guid? OrderId { get; set; }
}
