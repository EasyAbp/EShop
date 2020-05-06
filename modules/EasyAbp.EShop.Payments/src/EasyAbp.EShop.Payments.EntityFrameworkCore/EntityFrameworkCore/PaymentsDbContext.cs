using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Payments.EntityFrameworkCore
{
    [ConnectionStringName(PaymentsDbProperties.ConnectionStringName)]
    public class PaymentsDbContext : AbpDbContext<PaymentsDbContext>, IPaymentsDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePayments();
        }
    }
}