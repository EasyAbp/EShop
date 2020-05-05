using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderAppServiceTests : OrdersApplicationTestBase
    {
        private readonly IOrderAppService _orderAppService;

        public OrderAppServiceTests()
        {
            _orderAppService = GetRequiredService<IOrderAppService>();
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
