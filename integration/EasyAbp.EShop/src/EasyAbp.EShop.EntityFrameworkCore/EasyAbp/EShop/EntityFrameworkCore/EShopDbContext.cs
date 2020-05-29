using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.EntityFrameworkCore
{
    [ConnectionStringName(EShopDbProperties.ConnectionStringName)]
    public class EShopDbContext : AbpDbContext<EShopDbContext>, IEShopDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public EShopDbContext(DbContextOptions<EShopDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureEShop();
        }
    }
}