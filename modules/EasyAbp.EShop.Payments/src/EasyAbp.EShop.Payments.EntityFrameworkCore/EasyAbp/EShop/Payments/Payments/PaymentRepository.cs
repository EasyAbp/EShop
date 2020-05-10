using System;
using System.Linq;
using EasyAbp.EShop.Payments.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentRepository : EfCoreRepository<PaymentsDbContext, Payment, Guid>, IPaymentRepository
    {
        public PaymentRepository(IDbContextProvider<PaymentsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override IQueryable<Payment> WithDetails()
        {
            return base.WithDetails().Include(x => x.PaymentItems);
        }
    }
}