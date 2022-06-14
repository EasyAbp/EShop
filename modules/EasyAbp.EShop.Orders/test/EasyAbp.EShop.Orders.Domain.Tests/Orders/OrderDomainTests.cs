using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Refunds;
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
                0.3m
            ));
            Order1.SetPaymentId(OrderTestData.Payment1Id);
            Order1.SetPaidTime(DateTime.Now);
            
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
    }
}
