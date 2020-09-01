using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Stores.Transactions
{
    public class TransactionAppServiceTests : StoresApplicationTestBase
    {
        private readonly ITransactionAppService _transactionAppService;

        public TransactionAppServiceTests()
        {
            _transactionAppService = GetRequiredService<ITransactionAppService>();
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
