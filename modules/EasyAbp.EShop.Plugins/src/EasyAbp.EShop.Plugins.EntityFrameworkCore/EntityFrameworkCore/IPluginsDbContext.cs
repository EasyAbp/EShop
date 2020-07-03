using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.EntityFrameworkCore
{
    [ConnectionStringName(PluginsDbProperties.ConnectionStringName)]
    public interface IPluginsDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}