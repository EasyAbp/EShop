using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Volo.Abp.Timing;
using Xunit;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderAppServiceTests : OrdersApplicationTestBase
    {
        private readonly IClock _clock;
        private readonly IOrderAppService _orderAppService;

        public OrderAppServiceTests()
        {
            _clock = GetRequiredService<IClock>();
            _orderAppService = GetRequiredService<IOrderAppService>();
        }

        protected override void AfterAddApplication(IServiceCollection services)
        {
            var productAppService = Substitute.For<IProductAppService>();
            productAppService.GetAsync(OrderTestData.Product1Id).Returns(Task.FromResult(new ProductDto
            {
                CreationTime = DateTime.Now,
                IsPublished = true,
                Id = OrderTestData.Product1Id,
                StoreId = OrderTestData.Store1Id,
                ProductGroupName = "Default",
                ProductGroupDisplayName = "Default",
                UniqueName = "Pencil",
                DisplayName = "Hello pencil",
                ProductDetailId = OrderTestData.ProductDetail1Id,
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
                        ProductDetailId = null
                    },
                    new ProductSkuDto
                    {
                        Id = OrderTestData.ProductSku2Id,
                        Name = "My SKU 2",
                        OrderMinQuantity = 0,
                        OrderMaxQuantity = 100,
                        AttributeOptionIds = new List<Guid>(),
                        Price = 2m,
                        Currency = "CNY",
                        ProductDetailId = OrderTestData.ProductDetail2Id
                    }
                },
                InventoryStrategy = InventoryStrategy.NoNeed,
                LastModificationTime = OrderTestData.ProductLastModificationTime
            }));

            services.AddTransient(_ => productAppService);
            
            var productDetailAppService = Substitute.For<IProductDetailAppService>();

            productDetailAppService.GetAsync(OrderTestData.ProductDetail1Id).Returns(Task.FromResult(
                new ProductDetailDto
                {
                    Id = OrderTestData.ProductDetail1Id,
                    CreationTime = OrderTestData.ProductDetailLastModificationTime,
                    LastModificationTime = OrderTestData.ProductDetailLastModificationTime,
                    StoreId = OrderTestData.Store1Id,
                    Description = "My Details 1"
                }));

            productDetailAppService.GetAsync(OrderTestData.ProductDetail2Id).Returns(Task.FromResult(
                new ProductDetailDto
                {
                    Id = OrderTestData.ProductDetail2Id,
                    StoreId = OrderTestData.Store1Id,
                    Description = "My Details 2"
                }));

            services.AddTransient(_ => productDetailAppService);
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

            OrderDto createResponse = null;
            // Act
            await WithUnitOfWorkAsync(async () =>
            {
                createResponse = await _orderAppService.CreateAsync(createOrderDto);
            });

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
            responseOrderLine.ProductDetailModificationTime.ShouldBe(OrderTestData.ProductDetailLastModificationTime);
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

        [Fact]
        public async Task Unpaid_Order_Should_Be_Auto_Canceled_When_Payment_Is_Canceled()
        {
            // Arrange
            await Order_Should_Be_Created();
            Guid orderId = Guid.Empty;
            UsingDbContext(db =>
            {
                var order = db.Orders.First();
                orderId = order.Id;
                order.SetPaymentId(Guid.NewGuid());
                db.SaveChanges();
            });

            // Act
            var now = _clock.Now;

            UsingDbContext(async db =>
            {
                var orderRepository = ServiceProvider.GetRequiredService<IOrderRepository>();
                var order = await orderRepository.GetAsync(orderId);
                order.SetPaymentExpiration(now);
                order.SetPaymentId(null);
                await orderRepository.UpdateAsync(order, true);
            });
            
            var response = await _orderAppService.GetAsync(orderId);

            // Assert
            response.ShouldNotBeNull();
            response.PaymentId.ShouldBeNull();
            response.PaymentExpiration.ShouldBe(now);
            response.OrderStatus.ShouldBe(OrderStatus.Canceled);
            response.CanceledTime.ShouldNotBeNull();
            response.CancellationReason.ShouldBe(OrdersConsts.CancellationReason);
            
            UsingDbContext(db =>
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
                order.ShouldNotBeNull();
                order.PaymentId.ShouldBeNull();
                order.PaymentExpiration.ShouldBe(now);
                order.OrderStatus.ShouldBe(OrderStatus.Canceled);
                order.CanceledTime.ShouldNotBeNull();
                order.CancellationReason.ShouldBe(OrdersConsts.CancellationReason);
            });
        }

                [Fact]
        public async Task Paid_Order_Should_Not_Be_Auto_Canceled_When_Payment_Overtime()
        {
            // Arrange
            await Order_Should_Be_Created();
            Guid orderId = Guid.Empty;
            UsingDbContext(db =>
            {
                var order = db.Orders.First();
                orderId = order.Id;
                order.SetPaymentId(Guid.NewGuid());
                order.SetPaidTime(_clock.Now);
                order.SetOrderStatus(OrderStatus.Processing);
                db.SaveChanges();
            });

            // Act
            var now = _clock.Now;

            UsingDbContext(async db =>
            {
                var orderRepository = ServiceProvider.GetRequiredService<IOrderRepository>();
                var order = await orderRepository.GetAsync(orderId);
                order.SetPaymentExpiration(now);
                await orderRepository.UpdateAsync(order, true);
            });

            UsingDbContext(async db =>
            {
                var backgroundJob = ServiceProvider.GetRequiredService<UnpaidOrderAutoCancelJob>();
                await backgroundJob.ExecuteAsync(new UnpaidOrderAutoCancelArgs
                {
                    TenantId = null,
                    OrderId = orderId
                });
            });

            var response = await _orderAppService.GetAsync(orderId);

            // Assert
            response.ShouldNotBeNull();
            response.PaymentId.ShouldNotBeNull();
            response.PaymentExpiration.ShouldBe(now);
            response.OrderStatus.ShouldBe(OrderStatus.Processing);
            response.CanceledTime.ShouldBeNull();
            response.CancellationReason.ShouldBeNull();
            
            UsingDbContext(db =>
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
                order.ShouldNotBeNull();
                order.PaymentId.ShouldNotBeNull();
                order.PaymentExpiration.ShouldBe(now);
                order.OrderStatus.ShouldBe(OrderStatus.Processing);
                order.CanceledTime.ShouldBeNull();
                order.CancellationReason.ShouldBeNull();
            });
        }
        
        [Fact]
        public async Task Unpaid_Order_Should_Be_Auto_Canceled_When_Payment_Overtime()
        {
            // Arrange
            await Order_Should_Be_Created();
            Guid orderId = Guid.Empty;
            UsingDbContext(db =>
            {
                var order = db.Orders.First();
                orderId = order.Id;
            });

            // Act
            var now = _clock.Now;

            UsingDbContext(async db =>
            {
                var orderRepository = ServiceProvider.GetRequiredService<IOrderRepository>();
                var order = await orderRepository.GetAsync(orderId);
                order.SetPaymentExpiration(now);
                await orderRepository.UpdateAsync(order, true);
            });

            UsingDbContext(async db =>
            {
                var backgroundJob = ServiceProvider.GetRequiredService<UnpaidOrderAutoCancelJob>();
                await backgroundJob.ExecuteAsync(new UnpaidOrderAutoCancelArgs
                {
                    TenantId = null,
                    OrderId = orderId
                });
            });

            var response = await _orderAppService.GetAsync(orderId);

            // Assert
            response.ShouldNotBeNull();
            response.PaymentId.ShouldBeNull();
            response.PaymentExpiration.ShouldBe(now);
            response.OrderStatus.ShouldBe(OrderStatus.Canceled);
            response.CanceledTime.ShouldNotBeNull();
            response.CancellationReason.ShouldBe(OrdersConsts.CancellationReason);
            
            UsingDbContext(db =>
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
                order.ShouldNotBeNull();
                order.PaymentId.ShouldBeNull();
                order.PaymentExpiration.ShouldBe(now);
                order.OrderStatus.ShouldBe(OrderStatus.Canceled);
                order.CanceledTime.ShouldNotBeNull();
                order.CancellationReason.ShouldBe(OrdersConsts.CancellationReason);
            });
        }
        
        [Fact]
        public async Task Payment_Pending_Order_Should_Be_Auto_Canceled_When_Payment_Overtime()
        {
            // Arrange
            await Order_Should_Be_Created();
            Guid orderId = Guid.Empty;
            UsingDbContext(db =>
            {
                var order = db.Orders.First();
                orderId = order.Id;
                order.SetPaymentId(Guid.NewGuid());
                db.SaveChanges();
            });

            // Act
            var now = _clock.Now;

            UsingDbContext(async db =>
            {
                var orderRepository = ServiceProvider.GetRequiredService<IOrderRepository>();
                var order = await orderRepository.GetAsync(orderId);
                order.SetPaymentExpiration(now);
                await orderRepository.UpdateAsync(order, true);
            });

            UsingDbContext(async db =>
            {
                var backgroundJob = ServiceProvider.GetRequiredService<UnpaidOrderAutoCancelJob>();
                await backgroundJob.ExecuteAsync(new UnpaidOrderAutoCancelArgs
                {
                    TenantId = null,
                    OrderId = orderId
                });
            });

            var response = await _orderAppService.GetAsync(orderId);

            // Assert
            response.ShouldNotBeNull();
            response.PaymentId.ShouldBeNull();
            response.PaymentExpiration.ShouldBe(now);
            response.OrderStatus.ShouldBe(OrderStatus.Canceled);
            response.CanceledTime.ShouldNotBeNull();
            response.CancellationReason.ShouldBe(OrdersConsts.CancellationReason);
            
            UsingDbContext(db =>
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
                order.ShouldNotBeNull();
                order.PaymentId.ShouldBeNull();
                order.PaymentExpiration.ShouldBe(now);
                order.OrderStatus.ShouldBe(OrderStatus.Canceled);
                order.CanceledTime.ShouldNotBeNull();
                order.CancellationReason.ShouldBe(OrdersConsts.CancellationReason);
            });
        }
    }
}