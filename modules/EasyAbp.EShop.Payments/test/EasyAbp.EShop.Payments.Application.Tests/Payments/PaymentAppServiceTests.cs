using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Payments.Payments.Dtos;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentAppServiceTests : PaymentsApplicationTestBase
    {
        private readonly IPaymentAppService _paymentAppService;

        protected override void AfterAddApplication(IServiceCollection services)
        {
            MockOrderService(services);
        }

        private void MockOrderService(IServiceCollection services)
        {
            var orderService = Substitute.For<IOrderAppService>();
            orderService.GetAsync(PaymentsTestData.Order1).Returns(Task.FromResult(new OrderDto
            {
                Id = PaymentsTestData.Order1,
                Currency = "$",
                TotalDiscount = 10,
                TotalPrice = 100,
                StoreId = PaymentsTestData.Store1
            }));
            
            services.AddTransient(_ => orderService);
        }

        public PaymentAppServiceTests()
        {
            _paymentAppService = GetRequiredService<IPaymentAppService>();
        }

        [Fact]
        public async Task Should_Success_Create_Payment()
        {
            // Arrange
            var request = new CreatePaymentDto
            {
                OrderIds = new List<Guid>
                {
                    PaymentsTestData.Order1
                },
                PaymentMethod = "Alipay"
            };

            // Act & Assert
            await _paymentAppService.CreateAsync(request);
        }
    }
}
