using System;
using System.Linq;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Payments.Refunds
{
    public interface IRefundRepository : IRepository<Refund, Guid>
    {
        IQueryable<Refund> GetQueryableByUserId(Guid userId);
    }
}