using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.FlashSales.EntityFrameworkCore;

[ConnectionStringName(FlashSalesDbProperties.ConnectionStringName)]
public interface IFlashSalesDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
