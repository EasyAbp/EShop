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

        private readonly Guid _storeId = Guid.Parse("982C6439-AAC4-CD86-21E0-D89DFE066305");
        private readonly Guid _productId = Guid.Parse("309A5529-A42A-9E0F-85FA-879B17B70EA1");
        private readonly Guid _productSkuId = Guid.Parse("309A5529-A42A-9E0F-85FA-879B17B70EA2");

        public OrderAppServiceTests()
        {
            _orderAppService = GetRequiredService<IOrderAppService>();
        }

        protected override void AfterAddApplication(IServiceCollection services)
        {
            var productAppService = Substitute.For<IProductAppService>();
            productAppService.GetAsync(_productId, _storeId).Returns(Task.FromResult(new ProductDto
            {
                CreationTime = DateTime.Now,
                IsPublished = true,
                Id = _productId,
                ProductSkus = new List<ProductSkuDto>
                {
                    new ProductSkuDto
                    {
                        Id = _productSkuId,
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
                StoreId = _storeId,
                OrderLines = new List<CreateOrderLineDto>
                {
                    new CreateOrderLineDto
                    {
                        ProductId = _productId,
                        ProductSkuId = _productSkuId,
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
    }
}