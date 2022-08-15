using System;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class FlashSaleOrderCreationResultEto : ExtensibleObject, IMultiTenant
{
    public Guid? TenantId { get; set; }

    public Guid ResultId { get; set; }

    public bool Success { get; set; }

    public Guid StoreId { get; set; }

    public Guid PlanId { get; set; }

    public string Reason { get; set; }

    public Guid UserId { get; set; }

    public Guid? OrderId { get; set; }

    public bool AllowToTryAgain { get; set; }
}
