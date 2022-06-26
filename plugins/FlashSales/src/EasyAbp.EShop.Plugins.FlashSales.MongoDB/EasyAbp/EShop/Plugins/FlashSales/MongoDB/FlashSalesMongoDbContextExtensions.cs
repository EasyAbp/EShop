using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.FlashSales.MongoDB;

public static class FlashSalesMongoDbContextExtensions
{
    public static void ConfigureFlashSales(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<FlashSalesPlan>(b => b.CollectionName = FlashSalesDbProperties.DbTablePrefix + "Plans");

        builder.Entity<FlashSalesResult>(b => b.CollectionName = FlashSalesDbProperties.DbTablePrefix + "Results");
    }
}
