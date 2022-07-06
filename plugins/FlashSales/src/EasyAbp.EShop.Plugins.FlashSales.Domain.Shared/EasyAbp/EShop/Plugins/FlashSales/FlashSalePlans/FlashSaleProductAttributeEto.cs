using System;
using System.Collections.Generic;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

[Serializable]
public class FlashSaleProductAttributeEto : FullAuditedEntityEto<Guid>, IProductAttribute
{
    public string DisplayName { get; set; }

    public string Description { get; set; }

    public int DisplayOrder { get; set; }

    public List<FlashSaleProductAttributeOptionEto> ProductAttributeOptions { get; set; }
}
