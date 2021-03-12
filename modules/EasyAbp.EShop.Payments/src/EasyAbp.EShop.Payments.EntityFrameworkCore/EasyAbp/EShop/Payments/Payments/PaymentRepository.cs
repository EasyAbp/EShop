using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentRepository : EfCoreRepository<IPaymentsDbContext, Payment, Guid>, IPaymentRepository
    {
        public PaymentRepository(IDbContextProvider<IPaymentsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<Payment>> WithDetailsAsync()
        {
            return (await base.WithDetailsAsync()).Include(x => x.PaymentItems);
        }
    }
}