using System.Collections.Generic;
using EasyAbp.EShop.Products.Products;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes.SimpleProductDiscount;

public class SimpleProductDiscountModel : IHasProductScopes, IHasDynamicDiscountAmount
{
    public List<ProductScopeModel> ProductScopes { get; set; } = new();

    public DynamicDiscountAmountModel DynamicDiscountAmount { get; set; }

    public SimpleProductDiscountModel()
    {
    }

    public SimpleProductDiscountModel(List<ProductScopeModel> productScopes,
        DynamicDiscountAmountModel dynamicDiscountAmount)
    {
        if (!productScopes.IsNullOrEmpty())
        {
            ProductScopes = productScopes;
        }

        DynamicDiscountAmount = Check.NotNull(dynamicDiscountAmount, nameof(dynamicDiscountAmount));
    }
}