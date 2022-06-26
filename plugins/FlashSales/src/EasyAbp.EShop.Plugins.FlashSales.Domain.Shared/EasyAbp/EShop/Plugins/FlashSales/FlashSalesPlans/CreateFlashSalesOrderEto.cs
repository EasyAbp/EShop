using System;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

[Serializable]
public class CreateFlashSalesOrderEto : ExtensibleObject, IMultiTenant
{
    public Guid? TenantId { get; set; }

    public Guid StoreId { get; set; }

    public Guid PlanId { get; set; }

    public Guid UserId { get; set; }

    public Guid PendingResultId { get; set; }

    public DateTime CreateTime { get; set; }

    public string CustomerRemark { get; set; }

    public int Quantity { get; set; }

    public FlashSalesProductEto Product { get; set; }

    public FlashSalesProductDetailEto ProductDetail { get; set; }

    public FlashSalesPlanEto Plan { get; set; }
}
