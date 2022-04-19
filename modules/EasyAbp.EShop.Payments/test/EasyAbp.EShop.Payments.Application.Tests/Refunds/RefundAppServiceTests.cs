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
using Volo.Abp.Data;
using Volo.Abp.Json;
using Volo.Abp.Validation;
using Xunit;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class RefundAppServiceTests : PaymentsApplicationTestBase
    {
        private readonly IJsonSerializer _jsonSerializer;
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
                paymentItemType.GetProperty(nameof(PaymentItem.ActualPaymentAmount))?.SetValue(paymentItem, 1m);
                paymentItemType.GetProperty(nameof(PaymentItem.ItemType))?.SetValue(paymentItem, PaymentsConsts.PaymentItemType);
                paymentItemType.GetProperty(nameof(PaymentItem.ItemKey))?.SetValue(paymentItem, PaymentsTestData.Order1.ToString());
                paymentItem.ExtraProperties.Add("StoreId", PaymentsTestData.Store1.ToString());

                var payment = Activator.CreateInstance(paymentType, true) as Payment;
                payment.ShouldNotBeNull();
                paymentType.GetProperty(nameof(Payment.Id))?.SetValue(payment, PaymentsTestData.Payment1);
                paymentType.GetProperty(nameof(Payment.Currency))?.SetValue(payment, "CNY");
                paymentType.GetProperty(nameof(Payment.ActualPaymentAmount))?.SetValue(payment, 1m);
                paymentType.GetProperty(nameof(Payment.PaymentItems))?.SetValue(payment, new List<PaymentItem> {paymentItem});

                return payment;
            });
            
            paymentRepository.GetAsync(PaymentsTestData.Payment2).Returns(x =>
            {
                var paymentType = typeof(Payment);
                var paymentItemType = typeof(PaymentItem);
                
                var paymentItem = Activator.CreateInstance(paymentItemType, true) as PaymentItem;
                paymentItem.ShouldNotBeNull();
                paymentItemType.GetProperty(nameof(PaymentItem.Id))?.SetValue(paymentItem, PaymentsTestData.PaymentItem1);
                paymentItemType.GetProperty(nameof(PaymentItem.ActualPaymentAmount))?.SetValue(paymentItem, 1m);
                // pending refund amount
                paymentItemType.GetProperty(nameof(PaymentItem.PendingRefundAmount))?.SetValue(paymentItem, 1m);
                paymentItemType.GetProperty(nameof(PaymentItem.ItemType))?.SetValue(paymentItem, PaymentsConsts.PaymentItemType);
                paymentItemType.GetProperty(nameof(PaymentItem.ItemKey))?.SetValue(paymentItem, PaymentsTestData.Order1.ToString());
                paymentItem.ExtraProperties.Add("StoreId", PaymentsTestData.Store1.ToString());

                var payment = Activator.CreateInstance(paymentType, true) as Payment;
                payment.ShouldNotBeNull();
                paymentType.GetProperty(nameof(Payment.Id))?.SetValue(payment, PaymentsTestData.Payment1);
                paymentType.GetProperty(nameof(Payment.Currency))?.SetValue(payment, "CNY");
                paymentType.GetProperty(nameof(Payment.ActualPaymentAmount))?.SetValue(payment, 1m);
                // pending refund amount
                paymentType.GetProperty(nameof(Payment.PendingRefundAmount))?.SetValue(payment, 1m);
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
                        ActualTotalPrice = 1m,
                        Quantity = 1
                    }
                },
                PaymentId = PaymentsTestData.Payment1
            }));
            
            services.AddTransient(_ => orderService);
        }
        
        public RefundAppServiceTests()
        {
            _jsonSerializer = GetRequiredService<IJsonSerializer>();
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
                    new()
                    {
                        CustomerRemark = "CustomerRemark",
                        OrderId = PaymentsTestData.Order1,
                        StaffRemark = "StaffRemark",
                        OrderLines = new List<OrderLineRefundInfoModel>
                        {
                            new()
                            {
                                OrderLineId = PaymentsTestData.OrderLine1,
                                Quantity = 1,
                                TotalAmount = 0.4m
                            }
                        },
                        OrderExtraFees = new List<OrderExtraFeeRefundInfoModel>
                        {
                            new()
                            {
                                Name = "Name",
                                Key = "Key",
                                TotalAmount = 0.6m
                            }
                        }
                    }
                }
            };

            // Act & Assert
            await _refundAppService.CreateAsync(request);
            
            _testRefundPaymentEventHandler.IsEventPublished.ShouldBe(true);

            var eventData = _testRefundPaymentEventHandler.EventData;
            eventData.ShouldNotBeNull();
            eventData.CreateRefundInput.RefundItems.Count.ShouldBe(1);
            
            var refundItem = eventData.CreateRefundInput.RefundItems[0];
            refundItem.GetProperty<Guid>("OrderId").ShouldBe(PaymentsTestData.Order1);

            var orderLines =
                _jsonSerializer.Deserialize<List<OrderLineRefundInfoModel>>(
                    refundItem.GetProperty<string>("OrderLines"));

            orderLines.Count.ShouldBe(1);
            orderLines[0].OrderLineId.ShouldBe(PaymentsTestData.OrderLine1);
            orderLines[0].Quantity.ShouldBe(1);
            orderLines[0].TotalAmount.ShouldBe(0.4m);
            
            var orderExtraFees =
                _jsonSerializer.Deserialize<List<OrderExtraFeeRefundInfoModel>>(
                    refundItem.GetProperty<string>("OrderExtraFees"));
            
            orderExtraFees.Count.ShouldBe(1);
            orderExtraFees[0].Name.ShouldBe("Name");
            orderExtraFees[0].Key.ShouldBe("Key");
            orderExtraFees[0].TotalAmount.ShouldBe(0.6m);
        }

        [Fact]
        public async Task Should_Avoid_Empty_Refund()
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
                        OrderLines = new List<OrderLineRefundInfoModel>(),          // empty
                        OrderExtraFees = new List<OrderExtraFeeRefundInfoModel>()   // empty
                    }
                }
            };

            // Act & Assert
            await Should.ThrowAsync<AbpValidationException>(async () =>
            {
                await _refundAppService.CreateAsync(request);
            }, "RefundItem.OrderLines and RefundItem.OrderExtraFees should not both be empty!");
        }
        
        [Fact]
        public async Task Should_Avoid_Over_Refund()
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
                    new()
                    {
                        CustomerRemark = "CustomerRemark",
                        OrderId = PaymentsTestData.Order1,
                        StaffRemark = "StaffRemark",
                        OrderLines = new List<OrderLineRefundInfoModel>
                        {
                            new()
                            {
                                OrderLineId = PaymentsTestData.OrderLine1,
                                Quantity = 1,
                                TotalAmount = 1m
                            }
                        },
                        OrderExtraFees = new List<OrderExtraFeeRefundInfoModel>
                        {
                            new()
                            {
                                Name = "Name",
                                Key = "Key",
                                TotalAmount = 0.1m
                            }
                        }
                    }
                }
            };

            // Act & Assert
            await Should.ThrowAsync<InvalidRefundAmountException>(async () =>
            {
                await _refundAppService.CreateAsync(request);
            });
        }

        [Fact]
        public async Task Should_Avoid_Concurrent_Refund()
        {
            // Arrange
            var request = new CreateEShopRefundInput
            {
                DisplayReason = "Reason",
                CustomerRemark = "Customer Remark",
                PaymentId = PaymentsTestData.Payment2,
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
                                TotalAmount = 1m
                            }
                        }
                    }
                }
            };

            // Act & Assert
            await Should.ThrowAsync<AnotherRefundTaskIsOnGoingException>(async () =>
            {
                await _refundAppService.CreateAsync(request);
            });
        }
        
        [Fact]
        public async Task Should_Avoid_Non_Positive_Refund_Amount()
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
                                TotalAmount = -1m
                            }
                        }
                    }
                }
            };

            // Act & Assert
            await Should.ThrowAsync<AbpValidationException>(async () =>
            {
                await _refundAppService.CreateAsync(request);
            }, "RefundAmount should be greater than 0.");
        }
    }
}
