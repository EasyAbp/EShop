using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Transactions;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Stores.EntityFrameworkCore.Transactions
{
    public class TransactionRepositoryTests : StoresEntityFrameworkCoreTestBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionRepositoryTests()
        {
            _transactionRepository = GetRequiredService<ITransactionRepository>();
        }

        [Fact]
        public async Task Test1()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                // Arrange

                // Act

                //Assert
            });
        }
    }
}
