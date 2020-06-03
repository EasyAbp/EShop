using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Refunds;

namespace EasyAbp.EShop.Payments.EntityFrameworkCore
{
    [ConnectionStringName(PaymentsDbProperties.ConnectionStringName)]
    public interface IPaymentsDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<Payment> Payments { get; set; }
        DbSet<PaymentItem> PaymentItems { get; set; }
        DbSet<Refund> Refunds { get; set; }
    }
}
