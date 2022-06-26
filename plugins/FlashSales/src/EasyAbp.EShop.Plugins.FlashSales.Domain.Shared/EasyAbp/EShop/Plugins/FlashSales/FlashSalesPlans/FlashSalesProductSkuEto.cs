using System;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public class FlashSalesProductSkuEto : FullAuditedEntityEto<Guid>, IProductSku
{
    public string SerializedAttributeOptionIds { get; set; }

    public string Name { get; set; }

    public string Currency { get; set; }

    public decimal? OriginalPrice { get; set; }

    public decimal Price { get; set; }

    public int OrderMinQuantity { get; set; }

    public int OrderMaxQuantity { get; set; }

    public string MediaResources { get; set; }

    public Guid? ProductDetailId { get; set; }
}