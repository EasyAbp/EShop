using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class RefundAppServiceTests : PaymentsApplicationTestBase
    {
        private readonly IRefundAppService _refundAppService;

        public RefundAppServiceTests()
        {
            _refundAppService = GetRequiredService<IRefundAppService>();
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
