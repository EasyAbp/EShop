using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentAppServiceTests : PaymentsApplicationTestBase
    {
        private readonly IPaymentAppService _paymentAppService;

        public PaymentAppServiceTests()
        {
            _paymentAppService = GetRequiredService<IPaymentAppService>();
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
