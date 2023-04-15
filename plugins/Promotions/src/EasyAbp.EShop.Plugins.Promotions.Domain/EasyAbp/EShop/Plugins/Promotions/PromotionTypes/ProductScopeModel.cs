using System;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes;

public class ProductScopeModel
{
    /// <summary>
    /// The specified product group is affected when this property is set.
    /// </summary>
    public string? ProductGroupName { get; set; }

    /// <summary>
    /// The specified product (including all SKUs) is affected when this property is set.
    /// </summary>
    public Guid? ProductId { get; set; }

    /// <summary>
    /// The specified product SKU is affected when this property is set.
    /// </summary>
    public Guid? ProductSkuId { get; set; }

    public string? CustomScope { get; set; }

    public ProductScopeModel()
    {
    }

    public ProductScopeModel(string? productGroupName, Guid? productId, Guid? productSkuId, string? customScope)
    {
        ProductGroupName = productGroupName;
        ProductId = productId;
        ProductSkuId = productSkuId;
        CustomScope = customScope;
    }
}