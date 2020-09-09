using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderAppServiceTests : OrdersApplicationTestBase
    {
        private readonly IOrderAppService _orderAppService;

        public OrderAppServiceTests()
        {
            _orderAppService = GetRequiredService<IOrderAppService>();
        }

        protected override void AfterAddApplication(IServiceCollection services)
        {
            var productAppService = Substitute.For<IProductAppService>();
            productAppService.GetAsync(OrderTestData.Product1Id, OrderTestData.Store1Id).Returns(Task.FromResult(new ProductDto
            {
                CreationTime = DateTime.Now,
                IsPublished = true,
                Id = OrderTestData.Product1Id,
                ProductGroupName = "Default",
                ProductGroupDisplayName = "Default",
                UniqueName = "Pencil",
                DisplayName = "Hello pencil",
                ProductSkus = new List<ProductSkuDto>
                {
                    new ProductSkuDto
                    {
                        Id = OrderTestData.ProductSku1Id,
                        Name = "My SKU",
                        OrderMinQuantity = 0,
                        OrderMaxQuantity = 100,
                        AttributeOptionIds = new List<Guid>(),
                        Price = 1m,
                        Currency = "CNY",
                    }
                },
                InventoryStrategy = InventoryStrategy.NoNeed,
                LastModificationTime = OrderTestData.ProductLastModificationTime
            }));

            services.AddTransient(_ => productAppService);
        }

        [Fact]
        public async Task Order_Should_Be_Created()
        {
            // Arrange
            var createOrderDto = new CreateOrderDto
            {
                CustomerRemark = "customer remark",
                StoreId = OrderTestData.Store1Id,
                OrderLines = new List<CreateOrderLineDto>
                {
                    new CreateOrderLineDto
                    {
                        ProductId = OrderTestData.Product1Id,
                        ProductSkuId = OrderTestData.ProductSku1Id,
                        Quantity = 10
                    }
                }
            };

            // Act
            var createResponse = await _orderAppService.CreateAsync(createOrderDto);
            var response = await _orderAppService.GetAsync(createResponse.Id);

            // Assert
            response.ShouldNotBeNull();
            response.Currency.ShouldBe("CNY");
            response.CanceledTime.ShouldBeNull();
            response.CancellationReason.ShouldBeNullOrEmpty();
            response.CompletionTime.ShouldBeNull();
            response.CustomerRemark.ShouldBe("customer remark");
            response.OrderNumber.ShouldNotBeNull();
            response.OrderStatus.ShouldBe(OrderStatus.Pending);
            response.PaidTime.ShouldBeNull();
            response.PaymentId.ShouldBeNull();
            response.RefundAmount.ShouldBe(0m);
            response.StaffRemark.ShouldBeNullOrEmpty();
            response.StoreId.ShouldBe(OrderTestData.Store1Id);
            response.TotalDiscount.ShouldBe(0m);
            response.TotalPrice.ShouldBe(10m);
            response.ActualTotalPrice.ShouldBe(10m);
            response.CustomerUserId.ShouldBe(Guid.Parse("2e701e62-0953-4dd3-910b-dc6cc93ccb0d"));
            response.ProductTotalPrice.ShouldBe(10m);
            response.ReducedInventoryAfterPaymentTime.ShouldBeNull();
            response.ReducedInventoryAfterPlacingTime.ShouldNotBeNull();
            response.OrderLines.Count.ShouldBe(1);

            var responseOrderLine = response.OrderLines.First();
            responseOrderLine.ProductId.ShouldBe(OrderTestData.Product1Id);
            responseOrderLine.ProductSkuId.ShouldBe(OrderTestData.ProductSku1Id);
            responseOrderLine.ProductDisplayName.ShouldBe("Hello pencil");
            responseOrderLine.ProductUniqueName.ShouldBe("Pencil");
            responseOrderLine.ProductGroupName.ShouldBe("Default");
            responseOrderLine.ProductGroupDisplayName.ShouldBe("Default");
            responseOrderLine.SkuName.ShouldBe("My SKU");
            responseOrderLine.UnitPrice.ShouldBe(1m);
            responseOrderLine.TotalPrice.ShouldBe(10m);
            responseOrderLine.TotalDiscount.ShouldBe(0m);
            responseOrderLine.ActualTotalPrice.ShouldBe(10m);
            responseOrderLine.Currency.ShouldBe("CNY");
            responseOrderLine.Quantity.ShouldBe(10);
            responseOrderLine.ProductModificationTime.ShouldBe(OrderTestData.ProductLastModificationTime);
            responseOrderLine.RefundAmount.ShouldBe(0m);
            responseOrderLine.RefundedQuantity.ShouldBe(0);

            UsingDbContext(context =>
            {
                context.Orders.Count().ShouldBe(1);
                var order = context.Orders.Include(x => x.OrderLines).First();
                order.ShouldNotBeNull();
                order.Currency.ShouldBe("CNY");
                order.CanceledTime.ShouldBeNull();
                order.CancellationReason.ShouldBeNullOrEmpty();
                order.CompletionTime.ShouldBeNull();
                order.CustomerRemark.ShouldBe("customer remark");
                order.OrderNumber.ShouldNotBeNull();
                order.OrderStatus.ShouldBe(OrderStatus.Pending);
                order.PaidTime.ShouldBeNull();
                order.PaymentId.ShouldBeNull();
                order.RefundAmount.ShouldBe(0m);
                order.StaffRemark.ShouldBeNullOrEmpty();
                order.StoreId.ShouldBe(OrderTestData.Store1Id);
                order.TotalDiscount.ShouldBe(0m);
                order.TotalPrice.ShouldBe(10m);
                order.ActualTotalPrice.ShouldBe(10m);
                order.CustomerUserId.ShouldBe(Guid.Parse("2e701e62-0953-4dd3-910b-dc6cc93ccb0d"));
                order.ProductTotalPrice.ShouldBe(10m);
                order.ReducedInventoryAfterPaymentTime.ShouldBeNull();
                order.ReducedInventoryAfterPlacingTime.ShouldNotBeNull();
                order.OrderLines.Count.ShouldBe(1);

                var orderLine = order.OrderLines.First();
                orderLine.ProductId.ShouldBe(OrderTestData.Product1Id);
                orderLine.ProductSkuId.ShouldBe(OrderTestData.ProductSku1Id);
                orderLine.ProductDisplayName.ShouldBe("Hello pencil");
                orderLine.ProductUniqueName.ShouldBe("Pencil");
                orderLine.ProductGroupName.ShouldBe("Default");
                orderLine.ProductGroupDisplayName.ShouldBe("Default");
                orderLine.SkuName.ShouldBe("My SKU");
                orderLine.UnitPrice.ShouldBe(1m);
                orderLine.TotalPrice.ShouldBe(10m);
                orderLine.TotalDiscount.ShouldBe(0m);
                orderLine.ActualTotalPrice.ShouldBe(10m);
                orderLine.Currency.ShouldBe("CNY");
                orderLine.Quantity.ShouldBe(10);
                orderLine.ProductModificationTime.ShouldBe(OrderTestData.ProductLastModificationTime);
                orderLine.RefundAmount.ShouldBe(0m);
                orderLine.RefundedQuantity.ShouldBe(0);
            });
        }

        [Fact]
        public async Task Order_Should_Be_Completed()
        {
            // Arrange
            await Order_Should_Be_Created();
            Guid orderId = Guid.Empty;
            UsingDbContext(db =>
            {
                var order = db.Orders.First();
                order.OrderStatus.ShouldNotBe(OrderStatus.Completed);
                order.CompletionTime.ShouldBeNull();
                orderId = order.Id;
                order.SetPaidTime(DateTime.Now);
                order.SetReducedInventoryAfterPaymentTime(DateTime.Now);
                db.SaveChanges();
            });

            // Act
            var response = await _orderAppService.CompleteAsync(orderId);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldBe(orderId);
            response.OrderStatus.ShouldBe(OrderStatus.Completed);
        }

        [Fact]
        public async Task Order_Should_Be_Canceled()
        {
            // Arrange
            await Order_Should_Be_Created();
            Guid orderId = Guid.Empty;
            UsingDbContext(db =>
            {
                var order = db.Orders.First();
                order.OrderStatus.ShouldNotBe(OrderStatus.Canceled);
                order.CanceledTime.ShouldBeNull();
                orderId = order.Id;
            });

            var cancelRequestDto = new CancelOrderInput
            {
                CancellationReason = "Repeat orders."
            };

            // Act
            var response = await _orderAppService.CancelAsync(orderId, cancelRequestDto);

            // Assert
            response.ShouldNotBeNull();
            response.OrderStatus.ShouldBe(OrderStatus.Canceled);
            response.CanceledTime.ShouldNotBeNull();
            response.CancellationReason.ShouldBe("Repeat orders.");
            
            UsingDbContext(db =>
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
                order.ShouldNotBeNull();
                order.OrderStatus.ShouldBe(OrderStatus.Canceled);
                order.CancellationReason.ShouldBe("Repeat orders.");
            });
        }
    }
}