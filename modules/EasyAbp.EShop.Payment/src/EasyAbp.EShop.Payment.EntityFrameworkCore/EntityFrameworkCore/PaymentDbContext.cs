using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Payment.EntityFrameworkCore
{
    [ConnectionStringName(PaymentDbProperties.ConnectionStringName)]
    public class PaymentDbContext : AbpDbContext<PaymentDbContext>, IPaymentDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePayment();
        }
    }
}