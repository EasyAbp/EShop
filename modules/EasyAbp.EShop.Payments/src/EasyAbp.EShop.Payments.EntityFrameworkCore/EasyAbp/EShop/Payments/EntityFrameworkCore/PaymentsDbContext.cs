using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Refunds;
using EasyAbp.PaymentService;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Payments.EntityFrameworkCore
{
    [ConnectionStringName(PaymentsDbProperties.ConnectionStringName)]
    public class PaymentsDbContext : AbpDbContext<PaymentsDbContext>, IPaymentsDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Refund> Refunds { get; set; }

        public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<Payment>(b =>
            {
                b.ToTable(PaymentServiceDbProperties.DbTablePrefix + "Payments");
                
                b.ConfigureByConvention();
            });
            
            builder.Entity<Refund>(b =>
            {
                b.ToTable(PaymentServiceDbProperties.DbTablePrefix + "Refunds");
                
                b.ConfigureByConvention();
            });

            builder.ConfigureEShopPayments();
        }
    }
}
