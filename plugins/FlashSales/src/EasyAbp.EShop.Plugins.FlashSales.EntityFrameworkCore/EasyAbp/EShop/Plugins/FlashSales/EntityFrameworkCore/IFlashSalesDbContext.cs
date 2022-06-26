using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.FlashSales.EntityFrameworkCore;

[ConnectionStringName(FlashSalesDbProperties.ConnectionStringName)]
public interface IFlashSalesDbContext : IEfCoreDbContext
{
    DbSet<FlashSalesPlan> Plans { get; set; }

    DbSet<FlashSalesResult> Results { get; set; }
}
