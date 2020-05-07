using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Refunds;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Payments.EntityFrameworkCore.Refunds
{
    public class RefundRepositoryTests : PaymentsEntityFrameworkCoreTestBase
    {
        private readonly IRepository<Refund, Guid> _refundRepository;

        public RefundRepositoryTests()
        {
            _refundRepository = GetRequiredService<IRepository<Refund, Guid>>();
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
