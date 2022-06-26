using System;
using System.Collections.Generic;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public class FlashSalesProductEto : FullAuditedEntityEto<Guid>, IProduct, IMultiTenant
{
    public Guid? TenantId { get; set; }

    public Guid StoreId { get; set; }

    public string ProductGroupName { get; set; }

    public Guid? ProductDetailId { get; set; }

    public string UniqueName { get; set; }

    public string DisplayName { get; set; }

    public InventoryStrategy InventoryStrategy { get; set; }

    public string InventoryProviderName { get; set; }

    public string MediaResources { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsPublished { get; set; }

    public bool IsStatic { get; set; }

    public bool IsHidden { get; set; }

    public List<FlashSalesProductAttributeEto> ProductAttributes { get; set; }

    public List<FlashSalesProductSkuEto> ProductSkus { get; set; }
}
