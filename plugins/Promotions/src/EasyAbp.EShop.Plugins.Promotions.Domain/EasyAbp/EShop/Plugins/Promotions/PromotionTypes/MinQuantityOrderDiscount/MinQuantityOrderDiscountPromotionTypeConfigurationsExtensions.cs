using EasyAbp.EShop.Plugins.Promotions.Options;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes.MinQuantityOrderDiscount;

public static class MinQuantityOrderDiscountPromotionTypeConfigurationsExtensions
{
    public static void AddMinQuantityOrderDiscountPromotionType(this PromotionTypeConfigurations configurations)
    {
        configurations.AddOrUpdate(new PromotionTypeDefinition(
            MinQuantityOrderDiscountPromotionHandler.MinQuantityOrderDiscountPromotionTypeName,
            typeof(MinQuantityOrderDiscountPromotionHandler)));
    }
}