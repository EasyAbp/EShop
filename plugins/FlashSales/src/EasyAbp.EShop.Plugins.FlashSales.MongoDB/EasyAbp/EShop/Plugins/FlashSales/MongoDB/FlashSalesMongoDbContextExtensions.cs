using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.FlashSales.MongoDB;

public static class FlashSalesMongoDbContextExtensions
{
    public static void ConfigureEShopPluginsFlashSales(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<FlashSalePlan>(b => b.CollectionName = FlashSalesDbProperties.DbTablePrefix + "FlashSalePlans");

        builder.Entity<FlashSaleResult>(b => b.CollectionName = FlashSalesDbProperties.DbTablePrefix + "FlashSaleResults");
    }
}
