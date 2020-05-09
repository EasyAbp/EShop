using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Payments.WeChatPay.EntityFrameworkCore
{
    [ConnectionStringName(WeChatPayDbProperties.ConnectionStringName)]
    public class WeChatPayDbContext : AbpDbContext<WeChatPayDbContext>, IWeChatPayDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public WeChatPayDbContext(DbContextOptions<WeChatPayDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureEShopWeChatPay();
        }
    }
}