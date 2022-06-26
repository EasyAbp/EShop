using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Plugins.FlashSales.EntityFrameworkCore;

public static class FlashSalesDbContextModelCreatingExtensions
{
    public static void ConfigureEShopPluginsFlashSales(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<FlashSalesPlan>(b =>
        {
            b.ToTable(FlashSalesDbProperties.DbTablePrefix + "Plans", FlashSalesDbProperties.DbSchema);
            b.ConfigureByConvention();

            /* Configure more properties here */
        });

        builder.Entity<FlashSalesResult>(b =>
        {
            b.ToTable(FlashSalesDbProperties.DbTablePrefix + "Results", FlashSalesDbProperties.DbSchema);
            b.ConfigureByConvention();

            /* Configure more properties here */
        });
    }
}
