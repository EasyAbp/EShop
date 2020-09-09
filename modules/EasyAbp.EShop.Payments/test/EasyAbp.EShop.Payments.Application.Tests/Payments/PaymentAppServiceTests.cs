using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Payments.Payments.Dtos;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentAppServiceTests : PaymentsApplicationTestBase
    {
        private readonly IPaymentAppService _paymentAppService;
        private readonly TestCreatePaymentEventHandler _testCreatePaymentEventHandler;

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
                Currency = "CNY",
                ActualTotalPrice = 0,
                StoreId = PaymentsTestData.Store1,
                OrderLines = new List<OrderLineDto>
                {
                    new OrderLineDto
                    {
                        Id = PaymentsTestData.OrderLine1,
                        Currency = "CNY",
                        ActualTotalPrice = 0,
                        Quantity = 1
                    }
                }
            }));
            
            services.AddTransient(_ => orderService);
        }

        public PaymentAppServiceTests()
        {
            _paymentAppService = GetRequiredService<IPaymentAppService>();
            _testCreatePaymentEventHandler = GetRequiredService<TestCreatePaymentEventHandler>();
        }

        [Fact]
        public async Task Should_Publish_Create_Payment_Event()
        {
            // Arrange
            var request = new CreatePaymentDto
            {
                OrderIds = new List<Guid>
                {
                    PaymentsTestData.Order1
                },
                PaymentMethod = "Free"
            };

            // Act & Assert
            await _paymentAppService.CreateAsync(request);
            
            _testCreatePaymentEventHandler.IsEventPublished.ShouldBe(true );
        }
    }
}
