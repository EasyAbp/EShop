using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Orders.EntityFrameworkCore.Orders
{
    public class OrderRepositoryTests : OrdersEntityFrameworkCoreTestBase
    {
        private readonly IRepository<Order, Guid> _orderRepository;

        public OrderRepositoryTests()
        {
            _orderRepository = GetRequiredService<IRepository<Order, Guid>>();
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
