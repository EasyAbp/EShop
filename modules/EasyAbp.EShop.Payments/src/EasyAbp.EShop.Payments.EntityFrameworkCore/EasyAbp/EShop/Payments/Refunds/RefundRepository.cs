using System;
using System.Linq;
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

        public override IQueryable<Refund> WithDetails()
        {
            return base.WithDetails().Include(x => x.RefundItems);
        }

        public IQueryable<Refund> GetQueryableByUserId(Guid userId)
        {
            return from refund in DbContext.Refunds
                join payment in DbContext.Payments on refund.PaymentId equals payment.Id
                where payment.UserId == userId
                select refund;
        }
    }
}