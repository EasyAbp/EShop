using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Coupons.EntityFrameworkCore
{
    [ConnectionStringName(CouponsDbProperties.ConnectionStringName)]
    public interface ICouponsDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}