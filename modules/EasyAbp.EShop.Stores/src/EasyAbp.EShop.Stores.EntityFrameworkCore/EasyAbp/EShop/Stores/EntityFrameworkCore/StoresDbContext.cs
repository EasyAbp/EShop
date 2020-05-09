using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Stores.Stores;

namespace EasyAbp.EShop.Stores.EntityFrameworkCore
{
    [ConnectionStringName(StoresDbProperties.ConnectionStringName)]
    public class StoresDbContext : AbpDbContext<StoresDbContext>, IStoresDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<Store> Stores { get; set; }

        public StoresDbContext(DbContextOptions<StoresDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureEShopStores();
        }
    }
}
