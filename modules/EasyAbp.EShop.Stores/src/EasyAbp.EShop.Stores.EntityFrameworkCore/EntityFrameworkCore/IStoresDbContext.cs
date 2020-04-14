using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Stores.EntityFrameworkCore
{
    [ConnectionStringName(StoresDbProperties.ConnectionStringName)]
    public interface IStoresDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}