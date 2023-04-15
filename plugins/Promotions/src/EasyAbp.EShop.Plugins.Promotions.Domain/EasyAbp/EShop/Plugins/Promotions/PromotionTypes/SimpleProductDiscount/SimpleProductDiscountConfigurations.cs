using System.Collections.Generic;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes.SimpleProductDiscount;

public class SimpleProductDiscountConfigurations
{
    public List<SimpleProductDiscountModel> Discounts { get; set; } = new();
}