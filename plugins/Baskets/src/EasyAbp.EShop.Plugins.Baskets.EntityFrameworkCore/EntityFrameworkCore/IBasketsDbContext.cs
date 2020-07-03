using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore
{
    [ConnectionStringName(BasketsDbProperties.ConnectionStringName)]
    public interface IBasketsDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}