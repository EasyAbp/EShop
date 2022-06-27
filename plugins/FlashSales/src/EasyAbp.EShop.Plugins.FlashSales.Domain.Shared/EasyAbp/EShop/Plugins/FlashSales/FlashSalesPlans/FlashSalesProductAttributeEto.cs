using System;
using System.Collections.Generic;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

[Serializable]
public class FlashSalesProductAttributeEto : FullAuditedEntityEto<Guid>, IProductAttribute
{
    public string DisplayName { get; set; }

    public string Description { get; set; }

    public int DisplayOrder { get; set; }

    public List<FlashSalesProductAttributeOptionEto> ProductAttributeOptions { get; set; }
}
