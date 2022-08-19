using System;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

[Serializable]
public class CreateFlashSaleOrderEto : ExtensibleObject, IMultiTenant
{
    public Guid? TenantId { get; set; }

    public Guid ResultId { get; set; }

    public Guid UserId { get; set; }

    public string CustomerRemark { get; set; }

    public FlashSalePlanEto Plan { get; set; }

    public string HashToken { get; set; }
}
