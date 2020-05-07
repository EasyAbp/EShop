using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Payments.Payments
{
    public interface IPaymentRepository : IRepository<Payment, Guid>
    {
    }
}