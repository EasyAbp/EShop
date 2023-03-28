using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Promotions.EntityFrameworkCore;

[ConnectionStringName(PromotionsDbProperties.ConnectionStringName)]
public interface IPromotionsDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
