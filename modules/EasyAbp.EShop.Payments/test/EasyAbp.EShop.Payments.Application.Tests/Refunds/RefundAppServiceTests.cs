using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using EasyAbp.PaymentService.Refunds;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.Core;
using Shouldly;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Json;
using Volo.Abp.Uow;
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

            var payment1Returns = (CallInfo _) =>
            {
                var paymentType = typeof(Payment);
                var paymentItemType = typeof(PaymentItem);
                
                var paymentItem = Activator.CreateInstance(paymentItemType, true) as PaymentItem;
                paymentItem.ShouldNotBeNull();
                paymentItemType.GetProperty(nameof(PaymentItem.Id))?.SetValue(paymentItem, PaymentsTestData.PaymentItem1);
                paymentItemType.GetProperty(nameof(PaymentItem.ActualPaymentAmount))?.SetValue(paymentItem, 1m);
                paymentItemType.GetProperty(nameof(PaymentItem.ItemType))?.SetValue(paymentItem, PaymentsConsts.PaymentItemType);
                paymentItemType.GetProperty(nameof(PaymentItem.ItemKey))?.SetValue(paymentItem, PaymentsTestData.Order1.ToString());
                paymentItem.ExtraProperties.Add(nameof(paymentItem.StoreId), PaymentsTestData.Store1);

                var payment = Activator.CreateInstance(paymentType, true) as Payment;
                payment.ShouldNotBeNull();
                paymentType.GetProperty(nameof(Payment.Id))?.SetValue(payment, PaymentsTestData.Payment1);
                paymentType.GetProperty(nameof(Payment.Currency))?.SetValue(payment, "CNY");
                paymentType.GetProperty(nameof(Payment.ActualPaymentAmount))?.SetValue(payment, 1m);
                paymentType.GetProperty(nameof(Payment.PaymentItems))?.SetValue(payment, new List<PaymentItem> {paymentItem});

                return payment;
            };

            var payment2Returns = (CallInfo _) =>
            {
                var paymentType = typeof(Payment);
                var paymentItemType = typeof(PaymentItem);

                var paymentItem = Activator.CreateInstance(paymentItemType, true) as PaymentItem;
                paymentItem.ShouldNotBeNull();
                paymentItemType.GetProperty(nameof(PaymentItem.Id))
                    ?.SetValue(paymentItem, PaymentsTestData.PaymentItem1);
                paymentItemType.GetProperty(nameof(PaymentItem.ActualPaymentAmount))?.SetValue(paymentItem, 1m);
                // pending refund amount
                paymentItemType.GetProperty(nameof(PaymentItem.PendingRefundAmount))?.SetValue(paymentItem, 1m);
                paymentItemType.GetProperty(nameof(PaymentItem.ItemType))
                    ?.SetValue(paymentItem, PaymentsConsts.PaymentItemType);
                paymentItemType.GetProperty(nameof(PaymentItem.ItemKey))
                    ?.SetValue(paymentItem, PaymentsTestData.Order1.ToString());
                paymentItem.ExtraProperties.Add(nameof(paymentItem.StoreId), PaymentsTestData.Store1);

                var payment = Activator.CreateInstance(paymentType, true) as Payment;
                payment.ShouldNotBeNull();
                paymentType.GetProperty(nameof(Payment.Id))?.SetValue(payment, PaymentsTestData.Payment1);
                paymentType.GetProperty(nameof(Payment.Currency))?.SetValue(payment, "CNY");
                paymentType.GetProperty(nameof(Payment.ActualPaymentAmount))?.SetValue(payment, 1m);
                // pending refund amount
                paymentType.GetProperty(nameof(Payment.PendingRefundAmount))?.SetValue(payment, 1m);
                paymentType.GetProperty(nameof(Payment.PaymentItems))
                    ?.SetValue(payment, new List<PaymentItem> { paymentItem });

                return payment;
            };

            paymentRepository.GetAsync(PaymentsTestData.Payment1).Returns(payment1Returns);
            paymentRepository.FindAsync(PaymentsTestData.Payment1).Returns(payment1Returns);
            
            paymentRepository.GetAsync(PaymentsTestData.Payment2).Returns(payment2Returns);
            paymentRepository.FindAsync(PaymentsTestData.Payment2).Returns(payment2Returns);
            
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
                    new()
                    {
                        Id = PaymentsTestData.OrderLine1,
                        Currency = "CNY",
                        ActualTotalPrice = 1m,
                        Quantity = 1
                    }
                },
                OrderExtraFees = new List<OrderExtraFeeDto>
                {
                    new()
                    {
                        Name = "Name",
                        Key = "Key",
                        Fee = 5m
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
            refundItem.GetProperty<Guid?>(nameof(RefundItem.OrderId)).ShouldBe(PaymentsTestData.Order1);

            var orderLines =
                _jsonSerializer.Deserialize<List<OrderLineRefundInfoModel>>(
                    refundItem.GetProperty<string>(nameof(RefundItem.OrderLines)));

            orderLines.Count.ShouldBe(1);
            orderLines[0].OrderLineId.ShouldBe(PaymentsTestData.OrderLine1);
            orderLines[0].Quantity.ShouldBe(1);
            orderLines[0].TotalAmount.ShouldBe(0.4m);
            
            var orderExtraFees =
                _jsonSerializer.Deserialize<List<OrderExtraFeeRefundInfoModel>>(
                    refundItem.GetProperty<string>(nameof(RefundItem.OrderExtraFees)));
            
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
        public async Task Should_Check_OrderLines_Exist_When_Refunding()
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
                                OrderLineId = Guid.NewGuid(),
                                Quantity = 1,
                                TotalAmount = 1m
                            }
                        }
                    }
                }
            };
        }

        [Fact]
        public async Task Should_Check_OrderExtraFees_Exist_When_Refunding()
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
                        OrderExtraFees = new List<OrderExtraFeeRefundInfoModel>
                        {
                            new()
                            {
                                Name = "FakeName",
                                Key = "FakeKey",
                                TotalAmount = 0.6m
                            }
                        }
                    }
                }
            };

            // Act & Assert
            await Should.ThrowAsync<OrderExtraFeeNotFoundException>(async () =>
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

        [Fact]
        [UnitOfWork]
        public virtual async Task Should_Sync_A_Refund_From_RefundEto()
        {
            var synchronizer = ServiceProvider.GetRequiredService<RefundSynchronizer>();

            var refundItem = new RefundItemEto
            {
                Id = PaymentsTestData.RefundItem1,
                PaymentItemId = PaymentsTestData.PaymentItem1,
                RefundAmount = 1.5m,
                CustomerRemark = "CustomerRemark1",
                StaffRemark = "StaffRemark1"
            };

            var now = DateTime.Now;
            
            refundItem.SetProperty(nameof(RefundItem.StoreId), PaymentsTestData.Store1);
            refundItem.SetProperty(nameof(RefundItem.OrderId), PaymentsTestData.Order1);
            refundItem.SetProperty(nameof(RefundItem.OrderLines), _jsonSerializer.Serialize(new List<OrderLineRefundInfoModel>
            {
                new()
                {
                    OrderLineId = PaymentsTestData.OrderLine1,
                    Quantity = 2,
                    TotalAmount = 1m
                }
            }));
            refundItem.SetProperty(nameof(RefundItem.OrderExtraFees), _jsonSerializer.Serialize(new List<OrderExtraFeeRefundInfoModel>
            {
                new()
                {
                    Name = "Name",
                    Key = "Key",
                    TotalAmount = 0.5m
                }
            }));

            await synchronizer.HandleEventAsync(new EntityCreatedEto<RefundEto>(new RefundEto
            {
                Id = PaymentsTestData.Refund1,
                TenantId = null,
                PaymentId = PaymentsTestData.Payment1,
                RefundPaymentMethod = null,
                ExternalTradingCode = "testcode",
                Currency = "CNY",
                RefundAmount = 1.5m,
                DisplayReason = "DisplayReason",
                CustomerRemark = "CustomerRemark",
                StaffRemark = "StaffRemark",
                CompletedTime = now,
                CanceledTime = null,
                RefundItems = new List<RefundItemEto> { refundItem }
            }));

            var refundDto = await _refundAppService.GetAsync(PaymentsTestData.Refund1);
            
            refundDto.PaymentId.ShouldBe(PaymentsTestData.Payment1);
            refundDto.ExternalTradingCode.ShouldBe("testcode");
            refundDto.Currency.ShouldBe("CNY");
            refundDto.RefundAmount.ShouldBe(1.5m);
            refundDto.DisplayReason.ShouldBe("DisplayReason");
            refundDto.CustomerRemark.ShouldBe("CustomerRemark");
            refundDto.StaffRemark.ShouldBe("StaffRemark");
            refundDto.CompletedTime.ShouldBe(now);
            refundDto.CanceledTime.ShouldBeNull();
            refundDto.RefundItems.Count.ShouldBe(1);

            var refundItemDto = refundDto.RefundItems.First();
            refundItemDto.Id.ShouldBe(PaymentsTestData.RefundItem1);
            refundItemDto.PaymentItemId.ShouldBe(PaymentsTestData.PaymentItem1);
            refundItemDto.RefundAmount.ShouldBe(1.5m);
            refundItemDto.CustomerRemark.ShouldBe("CustomerRemark1");
            refundItemDto.StaffRemark.ShouldBe("StaffRemark1");
            // Extra properties
            refundItemDto.StoreId.ShouldBe(PaymentsTestData.Store1);
            refundItemDto.OrderId.ShouldBe(PaymentsTestData.Order1);
            refundItemDto.OrderLines.Count.ShouldBe(1);
            var orderLineDto = refundItemDto.OrderLines.First();
            orderLineDto.OrderLineId.ShouldBe(PaymentsTestData.OrderLine1);
            orderLineDto.RefundedQuantity.ShouldBe(2);
            orderLineDto.RefundAmount.ShouldBe(1m);
            refundItemDto.OrderExtraFees.Count.ShouldBe(1);
            var orderExtraFee = refundItemDto.OrderExtraFees.First();
            orderExtraFee.Name.ShouldBe("Name");
            orderExtraFee.Key.ShouldBe("Key");
            orderExtraFee.RefundAmount.ShouldBe(0.5m);
        }
    }
}
