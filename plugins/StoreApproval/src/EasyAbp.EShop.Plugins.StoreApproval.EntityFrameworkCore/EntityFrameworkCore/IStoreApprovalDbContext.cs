using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Plugins.StoreApproval.StoreApplications;

namespace EasyAbp.EShop.Plugins.StoreApproval.EntityFrameworkCore
{
    [ConnectionStringName(StoreApprovalDbProperties.ConnectionStringName)]
    public interface IStoreApprovalDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<StoreApplication> StoreApplications { get; set; }
    }
}
