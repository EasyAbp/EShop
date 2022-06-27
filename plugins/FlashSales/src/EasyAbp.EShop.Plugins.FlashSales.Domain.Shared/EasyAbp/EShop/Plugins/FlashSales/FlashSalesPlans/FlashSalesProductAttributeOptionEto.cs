using System;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

[Serializable]
public class FlashSalesProductAttributeOptionEto : FullAuditedEntityEto<Guid>, IProductAttributeOption
{
    public string DisplayName { get; set; }

    public string Description { get; set; }

    public int DisplayOrder { get; set; }
}