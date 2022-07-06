using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.FlashSales.EntityFrameworkCore;

[ConnectionStringName(FlashSalesDbProperties.ConnectionStringName)]
public interface IFlashSalesDbContext : IEfCoreDbContext
{
    DbSet<FlashSalePlan> FlashSalePlans { get; set; }

    DbSet<FlashSaleResult> FlashSaleResults { get; set; }
}
