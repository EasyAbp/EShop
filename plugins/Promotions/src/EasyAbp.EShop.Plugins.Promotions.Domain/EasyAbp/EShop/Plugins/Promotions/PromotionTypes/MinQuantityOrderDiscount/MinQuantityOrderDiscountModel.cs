using System.Collections.Generic;
using EasyAbp.EShop.Products.Products;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes.MinQuantityOrderDiscount;

public class MinQuantityOrderDiscountModel : IHasProductScopes, IHasDynamicDiscountAmount
{
    public List<ProductScopeModel> ProductScopes { get; set; } = new();

    /// <summary>
    /// This discount takes effect when the quantity of the order line equals to or greater than <see cref="MinQuantity"/>.
    /// It's important to note that this promotion type only applies to one OrderLine.
    /// The promotion will not apply if you have multiple OrderLines and the total quantity of all OrderLines meets the condition.
    /// </summary>
    public int MinQuantity { get; set; }

    public DynamicDiscountAmountModel DynamicDiscountAmount { get; set; }

    public MinQuantityOrderDiscountModel()
    {
    }

    public MinQuantityOrderDiscountModel(List<ProductScopeModel> productScopes, int minQuantity,
        DynamicDiscountAmountModel dynamicDiscountAmount)
    {
        if (!productScopes.IsNullOrEmpty())
        {
            ProductScopes = productScopes;
        }

        MinQuantity = minQuantity;
        DynamicDiscountAmount = Check.NotNull(dynamicDiscountAmount, nameof(dynamicDiscountAmount));
    }
}