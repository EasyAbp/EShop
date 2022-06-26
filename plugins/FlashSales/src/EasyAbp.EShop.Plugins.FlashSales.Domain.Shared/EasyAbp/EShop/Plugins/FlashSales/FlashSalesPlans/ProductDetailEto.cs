using System;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public class FlashSalesProductDetailEto : FullAuditedEntityEto<Guid>
{
    public Guid? StoreId { get; set; }

    public string Description { get; set; }
}