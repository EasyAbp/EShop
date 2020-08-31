using System;
using EasyAbp.EShop.Stores.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Stores.Transactions
{
    public class TransactionRepository : EfCoreRepository<StoresDbContext, Transaction, Guid>, ITransactionRepository
    {
        public TransactionRepository(IDbContextProvider<StoresDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}