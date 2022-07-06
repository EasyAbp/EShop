using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.FlashSales.EntityFrameworkCore;

[ConnectionStringName(FlashSalesDbProperties.ConnectionStringName)]
public class FlashSalesDbContext : AbpDbContext<FlashSalesDbContext>, IFlashSalesDbContext
{
    public DbSet<FlashSalePlan> FlashSalePlans { get; set; }

    public DbSet<FlashSaleResult> FlashSaleResults { get; set; }

    public FlashSalesDbContext(DbContextOptions<FlashSalesDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureEShopPluginsFlashSales();
    }
}
