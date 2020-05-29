using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.EntityFrameworkCore
{
    [ConnectionStringName(EShopDbProperties.ConnectionStringName)]
    public interface IEShopDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}