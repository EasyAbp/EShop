using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class RefundAppServiceTests : PaymentsApplicationTestBase
    {
        private readonly IRefundAppService _refundAppService;
        private readonly TestRefundPaymentEventHandler _testRefundPaymentEventHandler;

        protected override void AfterAddApplication(IServiceCollection services)
        {
            MockPaymentRepository(services);
            MockOrderService(services);
        }

        private void MockPaymentRepository(IServiceCollection services)
        {
            var paymentRepository = Substitute.For<IPaymentRepository>();
            paymentRepository.GetAsync(PaymentsTestData.Payment1).Returns(x =>
            {
                var paymentType = typeof(Payment);
                var paymentItemType = typeof(PaymentItem);
                
                var paymentItem = Activator.CreateInstance(paymentItemType, true) as PaymentItem;
                paymentItem.ShouldNotBeNull();
                paymentItemType.GetProperty(nameof(PaymentItem.Id))?.SetValue(paymentItem, PaymentsTestData.PaymentItem1);
                paymentItemType.GetProperty(nameof(PaymentItem.ActualPaymentAmount))?.SetValue(paymentItem, 0m);
                paymentItemType.GetProperty(nameof(PaymentItem.ItemType))?.SetValue(paymentItem, PaymentsConsts.PaymentItemType);
                paymentItemType.GetProperty(nameof(PaymentItem.ItemKey))?.SetValue(paymentItem, PaymentsTestData.Order1.ToString());
                paymentItem.ExtraProperties.Add("StoreId", PaymentsTestData.Store1.ToString());

                var payment = Activator.CreateInstance(paymentType, true) as Payment;
                payment.ShouldNotBeNull();
                paymentType.GetProperty(nameof(Payment.Id))?.SetValue(payment, PaymentsTestData.Payment1);
                paymentType.GetProperty(nameof(Payment.Currency))?.SetValue(payment, "CNY");
                paymentType.GetProperty(nameof(Payment.ActualPaymentAmount))?.SetValue(payment, 0m);
                paymentType.GetProperty(nameof(Payment.PaymentItems))?.SetValue(payment, new List<PaymentItem> {paymentItem});

                return payment;
            });
            
            services.AddTransient(_ => paymentRepository);
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
                },
                PaymentId = PaymentsTestData.Payment1
            }));
            
            services.AddTransient(_ => orderService);
        }
        
        public RefundAppServiceTests()
        {
            _refundAppService = GetRequiredService<IRefundAppService>();
            _testRefundPaymentEventHandler = GetRequiredService<TestRefundPaymentEventHandler>();
        }

        // TODO: There may be BUG.
        [Fact]
        public async Task Should_Publish_Refund_Payment_Event()
        {
            // Arrange
            var request = new CreateEShopRefundInput
            {
                DisplayReason = "Reason",
                CustomerRemark = "Customer Remark",
                PaymentId = PaymentsTestData.Payment1,
                StaffRemark = "StaffRemark",
                RefundItems = new List<CreateEShopRefundItemInput>
                {
                    new CreateEShopRefundItemInput
                    {
                        CustomerRemark = "CustomerRemark",
                        OrderId = PaymentsTestData.Order1,
                        StaffRemark = "StaffRemark",
                        OrderLines = new List<OrderLineRefundInfoModel>
                        {
                            new OrderLineRefundInfoModel
                            {
                                OrderLineId = PaymentsTestData.OrderLine1,
                                Quantity = 1,
                                TotalAmount = 0
                            }
                        }
                    }
                }
            };

            // Act & Assert
            await _refundAppService.CreateAsync(request);
            
            _testRefundPaymentEventHandler.IsEventPublished.ShouldBe(true);
        }
    }
}
