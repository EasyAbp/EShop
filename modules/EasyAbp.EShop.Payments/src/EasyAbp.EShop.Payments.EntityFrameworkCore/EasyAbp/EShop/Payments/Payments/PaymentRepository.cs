using System;
using EasyAbp.EShop.Payments.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentRepository : EfCoreRepository<PaymentsDbContext, Payment, Guid>, IPaymentRepository
    {
        public PaymentRepository(IDbContextProvider<PaymentsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}