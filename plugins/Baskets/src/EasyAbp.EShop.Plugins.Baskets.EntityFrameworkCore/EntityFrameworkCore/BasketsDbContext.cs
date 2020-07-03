using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore
{
    [ConnectionStringName(BasketsDbProperties.ConnectionStringName)]
    public class BasketsDbContext : AbpDbContext<BasketsDbContext>, IBasketsDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public BasketsDbContext(DbContextOptions<BasketsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureBaskets();
        }
    }
}