using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Refunds;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderDomainTests : OrdersDomainTestBase
    {
        private Order Order1 { get; set; }

        private readonly IOrderRepository _orderRepository;
        private readonly IMoneyDistributor _moneyDistributor;

        public OrderDomainTests()
        {
            _orderRepository = ServiceProvider.GetRequiredService<IOrderRepository>();
            _moneyDistributor = ServiceProvider.GetRequiredService<IMoneyDistributor>();
        }

        protected override void AfterAddApplication(IServiceCollection services)
        {
            var orderRepository = Substitute.For<IOrderRepository>();
            Order1 = new Order(
                OrderTestData.Order1Id,
                null,
                OrderTestData.Store1Id,
                Guid.NewGuid(),
                "USD",
                1m,
                0m,
                1.36m,
                1.36m,
                null,
                null);
            Order1.OrderLines.Add(new OrderLine(
                OrderTestData.OrderLine1Id,
                OrderTestData.Product1Id,
                OrderTestData.ProductSku1Id,
                null,
                DateTime.Now,
                null,
                "Default",
                "Default",
                null,
                "Product 1",
                InventoryStrategy.NoNeed,
                null,
                null,
                null,
                "USD",
                0.53m,
                1.06m,
                0m,
                1.06m,
                2
            ));
            Order1.OrderExtraFees.Add(new OrderExtraFee(
                OrderTestData.Order1Id,
                "Name",
                "Key",
                "DisplayName",
                0.3m
            ));

            orderRepository.GetAsync(OrderTestData.Order1Id).Returns(Task.FromResult(Order1));

            services.AddTransient(_ => orderRepository);
        }

        [Fact]
        public async Task Should_Record_Refund()
        {
            var handler = ServiceProvider.GetRequiredService<RefundCompletedEventHandler>();

            await Order1.StartPaymentAsync(OrderTestData.Payment1Id, Order1.ActualTotalPrice,
                _moneyDistributor);
            Order1.SetPaid(DateTime.Now);

            await handler.HandleEventAsync(new EShopRefundCompletedEto
            {
                Refund = new EShopRefundEto
                {
                    Id = Guid.NewGuid(),
                    TenantId = null,
                    PaymentId = OrderTestData.Payment1Id,
                    Currency = "USD",
                    RefundAmount = 0.3m,
                    RefundItems = new List<EShopRefundItemEto>
                    {
                        new()
                        {
                            Id = Guid.NewGuid(),
                            PaymentItemId = Guid.NewGuid(),
                            RefundAmount = 0.3m,
                            StoreId = OrderTestData.Store1Id,
                            OrderId = OrderTestData.Order1Id,
                            OrderLines = new List<RefundItemOrderLineEto>
                            {
                                new()
                                {
                                    OrderLineId = OrderTestData.OrderLine1Id,
                                    RefundedQuantity = 1,
                                    RefundAmount = 0.2m
                                }
                            },
                            OrderExtraFees = new List<RefundItemOrderExtraFeeEto>
                            {
                                new()
                                {
                                    Name = "Name",
                                    Key = "Key",
                                    DisplayName = "DisplayName",
                                    RefundAmount = 0.1m
                                }
                            }
                        }
                    }
                }
            });

            Order1.RefundAmount.ShouldBe(0.3m);

            var orderLine1 = Order1.OrderLines.Single(x => x.Id == OrderTestData.OrderLine1Id);
            orderLine1.RefundAmount.ShouldBe(0.2m);
            orderLine1.RefundedQuantity.ShouldBe(1);

            var extraFee = Order1.OrderExtraFees.Single(
                x => x.Name == "Name" && x.Key == "Key" && x.DisplayName == "DisplayName");
            extraFee.RefundAmount.ShouldBe(0.1m);
        }

        [Fact]
        public async Task Should_Avoid_Non_Positive_Refund_Amount()
        {
            var handler = ServiceProvider.GetRequiredService<RefundCompletedEventHandler>();

            await Order1.StartPaymentAsync(OrderTestData.Payment1Id, Order1.ActualTotalPrice,
                _moneyDistributor);
            Order1.SetPaid(DateTime.Now);

            await Should.ThrowAsync<InvalidRefundAmountException>(async () =>
            {
                await handler.HandleEventAsync(new EShopRefundCompletedEto
                {
                    Refund = new EShopRefundEto
                    {
                        Id = Guid.NewGuid(),
                        TenantId = null,
                        PaymentId = OrderTestData.Payment1Id,
                        Currency = "USD",
                        RefundAmount = -1m,
                        RefundItems = new List<EShopRefundItemEto>
                        {
                            new()
                            {
                                Id = Guid.NewGuid(),
                                PaymentItemId = Guid.NewGuid(),
                                RefundAmount = -1m,
                                StoreId = OrderTestData.Store1Id,
                                OrderId = OrderTestData.Order1Id,
                                OrderLines = new List<RefundItemOrderLineEto>
                                {
                                    new()
                                    {
                                        OrderLineId = OrderTestData.OrderLine1Id,
                                        RefundedQuantity = 1,
                                        RefundAmount = -1m
                                    }
                                }
                            }
                        }
                    }
                });
            });
        }


        [Fact]
        public async Task Should_Support_Different_PaymentAmounts()
        {
            // paymentAmount < actualTotalPrice
            await Order1.StartPaymentAsync(OrderTestData.Payment1Id, 1.2m,
                _moneyDistributor);
            Order1.SetPaid(DateTime.Now);

            Order1.ActualTotalPrice.ShouldBe(1.36m);
            Order1.PaymentAmount.ShouldBe(1.2m);
            Order1.OrderLines[0].PaymentAmount.ShouldBe(0.93m + 0.01m);
            Order1.OrderExtraFees[0].PaymentAmount.ShouldBe(0.26m);

            // paymentAmount == actualTotalPrice
            await Order1.StartPaymentAsync(OrderTestData.Payment1Id, 1.36m,
                _moneyDistributor);
            Order1.SetPaid(DateTime.Now);

            Order1.ActualTotalPrice.ShouldBe(1.36m);
            Order1.PaymentAmount.ShouldBe(1.36m);
            Order1.OrderLines[0].PaymentAmount.ShouldBe(1.06m);
            Order1.OrderExtraFees[0].PaymentAmount.ShouldBe(0.3m);

            // paymentAmount > actualTotalPrice
            await Order1.StartPaymentAsync(OrderTestData.Payment1Id, 1.5m,
                _moneyDistributor);
            Order1.SetPaid(DateTime.Now);

            Order1.ActualTotalPrice.ShouldBe(1.36m);
            Order1.PaymentAmount.ShouldBe(1.5m);
            Order1.OrderLines[0].PaymentAmount.ShouldBe(1.16m + 0.01m);
            Order1.OrderExtraFees[0].PaymentAmount.ShouldBe(0.33m);
        }

        [Fact]
        public async Task Should_Avoid_Over_Quantity_Refund()
        {
            var handler = ServiceProvider.GetRequiredService<RefundCompletedEventHandler>();

            await Order1.StartPaymentAsync(OrderTestData.Payment1Id, Order1.ActualTotalPrice,
                _moneyDistributor);
            Order1.SetPaid(DateTime.Now);

            await Should.ThrowAsync<InvalidRefundQuantityException>(async () =>
            {
                await handler.HandleEventAsync(new EShopRefundCompletedEto
                {
                    Refund = new EShopRefundEto
                    {
                        Id = Guid.NewGuid(),
                        TenantId = null,
                        PaymentId = OrderTestData.Payment1Id,
                        Currency = "USD",
                        RefundAmount = 0.2m,
                        RefundItems = new List<EShopRefundItemEto>
                        {
                            new()
                            {
                                Id = Guid.NewGuid(),
                                PaymentItemId = Guid.NewGuid(),
                                RefundAmount = 0.2m,
                                StoreId = OrderTestData.Store1Id,
                                OrderId = OrderTestData.Order1Id,
                                OrderLines = new List<RefundItemOrderLineEto>
                                {
                                    new()
                                    {
                                        OrderLineId = OrderTestData.OrderLine1Id,
                                        RefundedQuantity = 3,
                                        RefundAmount = 0.2m
                                    }
                                }
                            }
                        }
                    }
                });
            });
        }

        [Fact]
        public async Task Should_Avoid_Over_Amount_Refund()
        {
            var handler = ServiceProvider.GetRequiredService<RefundCompletedEventHandler>();

            await Order1.StartPaymentAsync(OrderTestData.Payment1Id, Order1.ActualTotalPrice,
                _moneyDistributor);
            Order1.SetPaid(DateTime.Now);

            await Should.ThrowAsync<InvalidRefundAmountException>(async () =>
            {
                await handler.HandleEventAsync(new EShopRefundCompletedEto
                {
                    Refund = new EShopRefundEto
                    {
                        Id = Guid.NewGuid(),
                        TenantId = null,
                        PaymentId = OrderTestData.Payment1Id,
                        Currency = "USD",
                        RefundAmount = 1.04m,
                        RefundItems = new List<EShopRefundItemEto>
                        {
                            new()
                            {
                                Id = Guid.NewGuid(),
                                PaymentItemId = Guid.NewGuid(),
                                RefundAmount = 0.3m,
                                StoreId = OrderTestData.Store1Id,
                                OrderId = OrderTestData.Order1Id,
                                OrderLines = new List<RefundItemOrderLineEto>
                                {
                                    new()
                                    {
                                        OrderLineId = OrderTestData.OrderLine1Id,
                                        RefundedQuantity = 1,
                                        RefundAmount = 1.07m // 1.07m > 1.06m
                                    }
                                }
                            }
                        }
                    }
                });
            });

            await Should.ThrowAsync<InvalidRefundAmountException>(async () =>
            {
                await handler.HandleEventAsync(new EShopRefundCompletedEto
                {
                    Refund = new EShopRefundEto
                    {
                        Id = Guid.NewGuid(),
                        TenantId = null,
                        PaymentId = OrderTestData.Payment1Id,
                        Currency = "USD",
                        RefundAmount = 0.3m,
                        RefundItems = new List<EShopRefundItemEto>
                        {
                            new()
                            {
                                Id = Guid.NewGuid(),
                                PaymentItemId = Guid.NewGuid(),
                                RefundAmount = 0.31m,
                                StoreId = OrderTestData.Store1Id,
                                OrderId = OrderTestData.Order1Id,
                                OrderExtraFees = new List<RefundItemOrderExtraFeeEto>
                                {
                                    new()
                                    {
                                        Name = "Name",
                                        Key = "Key",
                                        DisplayName = "DisplayName",
                                        RefundAmount = 0.31m // 0.31m > 0.3m
                                    }
                                }
                            }
                        }
                    }
                });
            });
        }

        [Fact]
        public async Task Should_Forbid_Canceling_Order_During_Payment_State()
        {
            var orderManager = ServiceProvider.GetRequiredService<IOrderManager>();
            var order = await _orderRepository.GetAsync(OrderTestData.Order1Id);

            await Order1.StartPaymentAsync(Guid.NewGuid(), Order1.ActualTotalPrice, _moneyDistributor);
            order.PaidTime.ShouldBeNull();
            await Should.ThrowAsync<OrderIsInWrongStageException>(() => orderManager.CancelAsync(order, "my-reason"));
        }

        [Fact]
        public async Task Should_Forbid_Canceling_Order_During_Inventory_Reduction_State()
        {
            var orderManager = ServiceProvider.GetRequiredService<IOrderManager>();
            var order = await _orderRepository.GetAsync(OrderTestData.Order1Id);

            order.ReducedInventoryAfterPlacingTime.ShouldBeNull();
            await Should.ThrowAsync<OrderIsInWrongStageException>(() => orderManager.CancelAsync(order, "my-reason"));

            order.SetReducedInventoryAfterPlacingTime(DateTime.Now);
            await order.StartPaymentAsync(Guid.NewGuid(), order.ActualTotalPrice, _moneyDistributor);
            order.SetPaid(DateTime.Now);
            typeof(Order).GetProperty(nameof(Order.ReducedInventoryAfterPlacingTime))!.SetValue(Order1, null);
            typeof(Order).GetProperty(nameof(Order.OrderStatus))!.SetValue(Order1, OrderStatus.Pending);
            await Should.ThrowAsync<OrderIsInWrongStageException>(() => orderManager.CancelAsync(order, "my-reason"));
        }
    }
}