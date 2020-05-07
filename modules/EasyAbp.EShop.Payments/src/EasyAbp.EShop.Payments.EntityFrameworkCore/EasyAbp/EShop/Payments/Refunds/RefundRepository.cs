using System;
using EasyAbp.EShop.Payments.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class RefundRepository : EfCoreRepository<PaymentsDbContext, Refund, Guid>, IRefundRepository
    {
        public RefundRepository(IDbContextProvider<PaymentsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}