using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Payments.Refunds
{
    public interface IRefundRepository : IRepository<Refund, Guid>
    {
        Task<IQueryable<Refund>> GetQueryableByUserIdAsync(Guid userId);
    }
}