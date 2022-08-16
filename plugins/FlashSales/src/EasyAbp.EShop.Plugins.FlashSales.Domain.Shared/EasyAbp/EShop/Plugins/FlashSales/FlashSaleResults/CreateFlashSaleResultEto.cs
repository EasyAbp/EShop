using System;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

[Serializable]
public class CreateFlashSaleResultEto : ExtensibleObject, IMultiTenant
{
    public Guid? TenantId { get; set; }

    public Guid ResultId { get; set; }

    public Guid UserId { get; set; }

    public DateTime ReducedInventoryTime { get; set; }

    public string CustomerRemark { get; set; }

    public FlashSalePlanEto Plan { get; set; }

    public string ProductInventoryProviderName { get; set; }

    public string HashToken { get; set; }
}
