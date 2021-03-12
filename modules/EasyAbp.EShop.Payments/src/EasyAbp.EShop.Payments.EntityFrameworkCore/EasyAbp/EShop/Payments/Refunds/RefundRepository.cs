using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class RefundRepository : EfCoreRepository<IPaymentsDbContext, Refund, Guid>, IRefundRepository
    {
        public RefundRepository(IDbContextProvider<IPaymentsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<Refund>> WithDetailsAsync()
        {
            return (await base.WithDetailsAsync()).Include(x => x.RefundItems);
        }

        public virtual async Task<IQueryable<Refund>> GetQueryableByUserIdAsync(Guid userId)
        {
            return from refund in (await GetDbContextAsync()).Refunds
                join payment in (await GetDbContextAsync()).Payments on refund.PaymentId equals payment.Id
                where payment.UserId == userId
                select refund;
        }
    }
}