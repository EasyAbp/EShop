using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Promotions.MongoDB;

public static class PromotionsMongoDbContextExtensions
{
    public static void ConfigureEShopPluginsPromotions(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
