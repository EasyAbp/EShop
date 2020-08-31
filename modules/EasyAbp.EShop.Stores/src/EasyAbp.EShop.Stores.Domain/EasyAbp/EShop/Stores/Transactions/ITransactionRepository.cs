using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Stores.Transactions
{
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    {
    }
}