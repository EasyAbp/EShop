using System;
using System.Collections.Generic;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

[Serializable]
public class FlashSalesProductSkuEto : FullAuditedEntityEto<Guid>, IProductSku
{
    public List<Guid> AttributeOptionIds { get; set; }

    public string SerializedAttributeOptionIds { get; set; }

    public string Name { get; set; }

    public string Currency { get; set; }

    public decimal? OriginalPrice { get; set; }

    public decimal Price { get; set; }

    public int OrderMinQuantity { get; set; }

    public int OrderMaxQuantity { get; set; }

    public TimeSpan? PaymentExpireIn { get; set; }

    public string MediaResources { get; set; }

    public Guid? ProductDetailId { get; set; }
}