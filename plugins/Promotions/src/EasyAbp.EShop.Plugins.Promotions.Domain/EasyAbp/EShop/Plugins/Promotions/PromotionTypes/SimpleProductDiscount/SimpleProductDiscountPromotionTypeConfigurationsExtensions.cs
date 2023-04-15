using EasyAbp.EShop.Plugins.Promotions.Options;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes.SimpleProductDiscount;

public static class SimpleProductDiscountPromotionTypeConfigurationsExtensions
{
    public static void AddSimpleProductDiscountPromotionType(this PromotionTypeConfigurations configurations)
    {
        configurations.AddOrUpdate(new PromotionTypeDefinition(
            SimpleProductDiscountPromotionHandler.SimpleProductDiscountPromotionTypeName,
            typeof(SimpleProductDiscountPromotionHandler)));
    }
}