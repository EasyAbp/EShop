using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Plugins.Promotions.Promotions;

namespace EasyAbp.EShop.Plugins.Promotions.EntityFrameworkCore;

[ConnectionStringName(PromotionsDbProperties.ConnectionStringName)]
public interface IPromotionsDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    DbSet<Promotion> Promotions { get; set; }
}
