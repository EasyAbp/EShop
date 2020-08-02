using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Plugins.StoreApproval.StoreApplications;

namespace EasyAbp.EShop.Plugins.StoreApproval.EntityFrameworkCore
{
    [ConnectionStringName(StoreApprovalDbProperties.ConnectionStringName)]
    public class StoreApprovalDbContext : AbpDbContext<StoreApprovalDbContext>, IStoreApprovalDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<StoreApplication> StoreApplications { get; set; }

        public StoreApprovalDbContext(DbContextOptions<StoreApprovalDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureStoreApproval();
        }
    }
}
