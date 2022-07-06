using System;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

[Serializable]
public class CreateFlashSaleOrderEto : ExtensibleObject, IMultiTenant
{
    public Guid? TenantId { get; set; }

    public Guid StoreId { get; set; }

    public Guid PlanId { get; set; }

    public Guid UserId { get; set; }

    public Guid PendingResultId { get; set; }

    public DateTime CreateTime { get; set; }

    public string CustomerRemark { get; set; }

    public FlashSaleProductEto Product { get; set; }

    public FlashSaleProductDetailEto ProductDetail { get; set; }

    public FlashSalePlanEto Plan { get; set; }
}
