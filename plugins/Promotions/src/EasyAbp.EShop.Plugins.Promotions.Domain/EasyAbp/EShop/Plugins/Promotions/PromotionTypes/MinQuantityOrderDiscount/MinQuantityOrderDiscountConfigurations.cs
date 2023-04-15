using System.Collections.Generic;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes.MinQuantityOrderDiscount;

public class MinQuantityOrderDiscountConfigurations
{
    public List<MinQuantityOrderDiscountModel> Discounts { get; set; } = new();
}