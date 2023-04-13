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

        public OrderDomainTests()
        {
            _orderRepository = ServiceProvider.GetRequiredService<IOrderRepository>();
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
                1.5m,
                1.5m,
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
                0.5m,
                1m,
                0m,
                1m,
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
                                    RefundAmount = 0.1m
                                }
                            }
                        }
                    }
                }
            });

            Order1.SetPaymentId(OrderTestData.Payment1Id);
            Order1.SetPaidTime(DateTime.Now);

            Order1.RefundAmount.ShouldBe(0.3m);

            var orderLine1 = Order1.OrderLines.Single(x => x.Id == OrderTestData.OrderLine1Id);
            orderLine1.RefundAmount.ShouldBe(0.2m);
            orderLine1.RefundedQuantity.ShouldBe(1);

            var extraFee = Order1.OrderExtraFees.Single(x => x.Name == "Name" && x.Key == "Key");
            extraFee.RefundAmount.ShouldBe(0.1m);
        }

        [Fact]
        public async Task Should_Avoid_Non_Positive_Refund_Amount()
        {
            var handler = ServiceProvider.GetRequiredService<RefundCompletedEventHandler>();

            Order1.SetPaymentId(OrderTestData.Payment1Id);
            Order1.SetPaidTime(DateTime.Now);

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
        public async Task Should_Avoid_Over_Quantity_Refund()
        {
            var handler = ServiceProvider.GetRequiredService<RefundCompletedEventHandler>();

            Order1.SetPaymentId(OrderTestData.Payment1Id);
            Order1.SetPaidTime(DateTime.Now);

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
        public async Task Should_Forbid_Canceling_Order_During_Payment_State()
        {
            var orderManager = ServiceProvider.GetRequiredService<IOrderManager>();
            var order = await _orderRepository.GetAsync(OrderTestData.Order1Id);

            order.SetPaymentId(Guid.NewGuid());
            order.SetPaidTime(null);
            await Should.ThrowAsync<OrderIsInWrongStageException>(() => orderManager.CancelAsync(order, "my-reason"));
        }

        [Fact]
        public async Task Should_Forbid_Canceling_Order_During_Inventory_Reduction_State()
        {
            var orderManager = ServiceProvider.GetRequiredService<IOrderManager>();
            var order = await _orderRepository.GetAsync(OrderTestData.Order1Id);

            order.SetReducedInventoryAfterPlacingTime(null);
            await Should.ThrowAsync<OrderIsInWrongStageException>(() => orderManager.CancelAsync(order, "my-reason"));

            order.SetReducedInventoryAfterPlacingTime(DateTime.Now);
            order.SetPaymentId(Guid.NewGuid());
            order.SetPaidTime(DateTime.Now);
            order.SetReducedInventoryAfterPlacingTime(null);
            await Should.ThrowAsync<OrderIsInWrongStageException>(() => orderManager.CancelAsync(order, "my-reason"));
        }

        [Fact]
        public async Task Should_Discount_Multi_OrderLine()
        {
            var orderGenerator = GetRequiredService<INewOrderGenerator>();

            var createOrderInfoModel = new CreateOrderInfoModel(OrderTestData.Store1Id, null,
                new List<CreateOrderLineInfoModel>
                {
                    new(OrderTestData.Product1Id, OrderTestData.ProductSku1Id, 1),
                    new(OrderTestData.Product1Id, OrderTestData.ProductSku2Id, 1),
                }
            );

            var order = await orderGenerator.GenerateAsync(Guid.NewGuid(), createOrderInfoModel,
                new Dictionary<Guid, IProduct>
                {
                    {
                        OrderTestData.Product1Id, new ProductEto
                        {
                            Id = OrderTestData.Product1Id,
                            ProductSkus = new List<ProductSkuEto>
                            {
                                new()
                                {
                                    Id = OrderTestData.ProductSku1Id,
                                    AttributeOptionIds = new List<Guid>(),
                                    Price = 1.7m,
                                    Currency = "USD",
                                    OrderMinQuantity = 1,
                                    OrderMaxQuantity = 100,
                                },
                                new()
                                {
                                    Id = OrderTestData.ProductSku2Id,
                                    AttributeOptionIds = new List<Guid>(),
                                    Price = 3.11m,
                                    Currency = "USD",
                                    OrderMinQuantity = 1,
                                    OrderMaxQuantity = 100,
                                }
                            }
                        }
                    }
                }, new Dictionary<Guid, DateTime>());

            const decimal discountedAmount = 0.9m;

            order.AddDiscounts(new OrderDiscountInfoModel(order.OrderLines.Select(x => x.Id).ToList(), null, "Test",
                null, null, discountedAmount));

            var orderLine1 = order.OrderLines[0];
            var orderLine2 = order.OrderLines[1];
            var discountedAmount1 = Math.Round(discountedAmount * (orderLine1.TotalPrice / order.TotalPrice), 2,
                MidpointRounding.ToZero);
            var discountedAmount2 = Math.Round(discountedAmount * (orderLine2.TotalPrice / order.TotalPrice), 2,
                MidpointRounding.ToZero);
            var fraction = discountedAmount - discountedAmount1 - discountedAmount2;

            discountedAmount1.ShouldBe(0.31m);
            discountedAmount2.ShouldBe(0.58m);
            fraction.ShouldBe(0.01m);

            var discount1 = order.OrderDiscounts.Find(x => x.OrderLineId == orderLine1.Id);
            var discount2 = order.OrderDiscounts.Find(x => x.OrderLineId == orderLine2.Id);

            order.OrderDiscounts.Count.ShouldBe(2);
            discount1.ShouldNotBeNull();
            discount2.ShouldNotBeNull();

            discount1.DiscountedAmount.ShouldBe(discountedAmount1);
            discount2.DiscountedAmount.ShouldBe(discountedAmount2 + fraction);
        }
    }
}