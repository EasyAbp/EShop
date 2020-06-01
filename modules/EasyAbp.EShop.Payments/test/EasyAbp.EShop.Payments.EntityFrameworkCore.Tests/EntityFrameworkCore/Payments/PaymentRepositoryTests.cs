using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Payments;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Payments.EntityFrameworkCore.Payments
{
    public class PaymentRepositoryTests : PaymentsEntityFrameworkCoreTestBase
    {
        private readonly IRepository<Payment, Guid> _paymentRepository;

        public PaymentRepositoryTests()
        {
            _paymentRepository = GetRequiredService<IRepository<Payment, Guid>>();
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
