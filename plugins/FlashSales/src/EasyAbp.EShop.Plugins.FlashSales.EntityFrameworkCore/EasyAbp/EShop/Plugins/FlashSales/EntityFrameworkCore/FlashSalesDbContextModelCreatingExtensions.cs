using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
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

        builder.Entity<FlashSalePlan>(b =>
        {
            b.ToTable(FlashSalesDbProperties.DbTablePrefix + "FlashSalePlans", FlashSalesDbProperties.DbSchema);
            b.ConfigureByConvention();

            /* Configure more properties here */
        });

        builder.Entity<FlashSaleResult>(b =>
        {
            b.ToTable(FlashSalesDbProperties.DbTablePrefix + "FlashSaleResults", FlashSalesDbProperties.DbSchema);
            b.ConfigureByConvention();

            /* Configure more properties here */
        });
    }
}
