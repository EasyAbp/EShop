using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
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
                ProductSkus = new List<ProductSkuDto>
                {
                    new ProductSkuDto
                    {
                        Id = OrderTestData.ProductSku1Id,
                        OrderMinQuantity = 0,
                        OrderMaxQuantity = 100,
                        AttributeOptionIds = new List<Guid>()
                    }
                },
                InventoryStrategy = InventoryStrategy.NoNeed
            }));

            services.AddTransient(_ => productAppService);
        }

        [Fact]
        public async Task Should_Create_A_Order()
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
            var response = await _orderAppService.CreateAsync(createOrderDto);

            // Assert
            response.ShouldNotBeNull();
            response.CustomerRemark.ShouldBe("customer remark");

            UsingDbContext(context =>
            {
                context.Orders.Count().ShouldBe(1);
                var order = context.Orders.First();
                order.CustomerRemark.ShouldBe("customer remark");
            });
        }

        [Fact]
        public async Task Order_Should_Complete()
        {
            // Arrange
            await Should_Create_A_Order();
            Guid orderId = Guid.Empty;
            UsingDbContext(db =>
            {
                var order = db.Orders.First();
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
        public async Task Order_Should_Cancel()
        {
            // Arrange
            await Should_Create_A_Order();
            Guid orderId = Guid.Empty;
            UsingDbContext(db =>
            {
                var order = db.Orders.First();
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