using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.FlashSales.EntityFrameworkCore;

[ConnectionStringName(FlashSalesDbProperties.ConnectionStringName)]
public class FlashSalesDbContext : AbpDbContext<FlashSalesDbContext>, IFlashSalesDbContext
{
    public DbSet<FlashSalesPlan> Plans { get; set; }

    public DbSet<FlashSalesResult> Results { get; set; }

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
