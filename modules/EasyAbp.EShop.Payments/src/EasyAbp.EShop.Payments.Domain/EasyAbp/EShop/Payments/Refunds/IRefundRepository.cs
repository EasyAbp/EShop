using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Payments.Refunds
{
    public interface IRefundRepository : IRepository<Refund, Guid>
    {
    }
}