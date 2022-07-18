using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Orders.Settings;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Settings;
using Volo.Abp.Timing;
using Xunit;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderAppServiceTests : OrdersApplicationTestBase
    {
        private readonly IClock _clock;
        private readonly IOrderAppService _orderAppService;

        private ProductDto Product1 { get; set; }

        public OrderAppServiceTests()
        {
            _clock = GetRequiredService<IClock>();
            _orderAppService = GetRequiredService<IOrderAppService>();
        }

        protected override void AfterAddApplication(IServiceCollection services)
        {
            Product1 = new ProductDto
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
                        Currency = "USD",
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
                        Currency = "USD",
                        ProductDetailId = OrderTestData.ProductDetail2Id
                    },
                    new ProductSkuDto
                    {
                        Id = OrderTestData.ProductSku3Id,
                        Name = "My SKU 3",
                        OrderMinQuantity = 0,
                        OrderMaxQuantity = 100,
                        AttributeOptionIds = new List<Guid>(),
                        Price = 3m,
                        Currency = "USD",
                        ProductDetailId = OrderTestData.ProductDetail2Id
                    }
                },
                InventoryStrategy = InventoryStrategy.NoNeed,
                LastModificationTime = OrderTestData.ProductLastModificationTime
            };

            var productAppService = Substitute.For<IProductAppService>();
            productAppService.GetAsync(OrderTestData.Product1Id).Returns(Task.FromResult(Product1));

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
        public async Task Check_Create_Order_Should_Succeed()
        {
            var orderRepository = ServiceProvider.GetRequiredService<IOrderRepository>();
            var orderCount = 0;
            await WithUnitOfWorkAsync(async () =>
            {
                orderCount = await orderRepository.CountAsync();
            });

            // Arrange
            var checkCreateOrderInput = new CheckCreateOrderInput
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

            CheckCreateOrderResultDto resultDto = null;
            // Act
            await WithUnitOfWorkAsync(async () =>
            {
                resultDto = await _orderAppService.CheckCreateAsync(checkCreateOrderInput);
            });

            // Assert
            resultDto.CanCreate.ShouldBeTrue();
            await WithUnitOfWorkAsync(async () =>
            {
                orderCount.ShouldBeEquivalentTo(await orderRepository.CountAsync());
            });
        }

        [Fact]
        public async Task Check_Create_Order_Should_Fail()
        {
            var orderRepository = ServiceProvider.GetRequiredService<IOrderRepository>();
            var orderCount = 0;
            await WithUnitOfWorkAsync(async () =>
            {
                orderCount = await orderRepository.CountAsync();
            });

            // Arrange
            var checkCreateOrderInput = new CheckCreateOrderInput
            {
                CustomerRemark = "customer remark",
                StoreId = OrderTestData.Store1Id,
                OrderLines = new List<CreateOrderLineDto>
                {
                    new CreateOrderLineDto
                    {
                        ProductId = OrderTestData.Product1Id,
                        ProductSkuId = OrderTestData.ProductSku1Id,
                        Quantity = 101 // limited range: 1-100
                    }
                }
            };

            CheckCreateOrderResultDto resultDto = null;
            // Act
            await WithUnitOfWorkAsync(async () =>
            {
                resultDto = await _orderAppService.CheckCreateAsync(checkCreateOrderInput);
            });

            // Assert
            resultDto.CanCreate.ShouldBeFalse();
            await WithUnitOfWorkAsync(async () =>
            {
                orderCount.ShouldBeEquivalentTo(await orderRepository.CountAsync());
            });
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
                    },
                    new CreateOrderLineDto
                    {
                        ProductId = OrderTestData.Product1Id,
                        ProductSkuId = OrderTestData.ProductSku2Id,
                        Quantity = 1
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
            UsingDbContext(context =>
            {
                context.Orders.Count().ShouldBe(1);
                var order = context.Orders.Include(x => x.OrderLines).First();
                order.ShouldNotBeNull();
                order.Currency.ShouldBe("USD");
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
                order.TotalPrice.ShouldBe(12m);
                order.ActualTotalPrice.ShouldBe(12m);
                order.CustomerUserId.ShouldBe(Guid.Parse("2e701e62-0953-4dd3-910b-dc6cc93ccb0d"));
                order.ProductTotalPrice.ShouldBe(12m);
                order.ReducedInventoryAfterPaymentTime.ShouldBeNull();
                order.ReducedInventoryAfterPlacingTime.ShouldNotBeNull();
                order.OrderLines.Count.ShouldBe(2);

                var orderLine1 = response.OrderLines.Single(x => x.ProductSkuId == OrderTestData.ProductSku1Id);
                orderLine1.ProductId.ShouldBe(OrderTestData.Product1Id);
                orderLine1.ProductSkuId.ShouldBe(OrderTestData.ProductSku1Id);
                orderLine1.ProductDetailId.ShouldBe(OrderTestData.ProductDetail1Id);
                orderLine1.ProductDisplayName.ShouldBe("Hello pencil");
                orderLine1.ProductUniqueName.ShouldBe("Pencil");
                orderLine1.ProductGroupName.ShouldBe("Default");
                orderLine1.ProductGroupDisplayName.ShouldBe("Default");
                orderLine1.SkuName.ShouldBe("My SKU");
                orderLine1.UnitPrice.ShouldBe(1m);
                orderLine1.TotalPrice.ShouldBe(10m);
                orderLine1.TotalDiscount.ShouldBe(0m);
                orderLine1.ActualTotalPrice.ShouldBe(10m);
                orderLine1.Currency.ShouldBe("USD");
                orderLine1.Quantity.ShouldBe(10);
                orderLine1.ProductModificationTime.ShouldBe(OrderTestData.ProductLastModificationTime);
                orderLine1.ProductDetailModificationTime.ShouldBe(OrderTestData.ProductDetailLastModificationTime);
                orderLine1.RefundAmount.ShouldBe(0m);
                orderLine1.RefundedQuantity.ShouldBe(0);

                var orderLine2 = response.OrderLines.Single(x => x.ProductSkuId == OrderTestData.ProductSku2Id);
                orderLine2.ProductDetailId.ShouldBe(OrderTestData.ProductDetail2Id);
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
            response.CancellationReason.ShouldBe(OrdersConsts.UnpaidAutoCancellationReason);

            UsingDbContext(db =>
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
                order.ShouldNotBeNull();
                order.PaymentId.ShouldBeNull();
                order.PaymentExpiration.ShouldBe(now);
                order.OrderStatus.ShouldBe(OrderStatus.Canceled);
                order.CanceledTime.ShouldNotBeNull();
                order.CancellationReason.ShouldBe(OrdersConsts.UnpaidAutoCancellationReason);
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
            response.CancellationReason.ShouldBe(OrdersConsts.UnpaidAutoCancellationReason);

            UsingDbContext(db =>
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
                order.ShouldNotBeNull();
                order.PaymentId.ShouldBeNull();
                order.PaymentExpiration.ShouldBe(now);
                order.OrderStatus.ShouldBe(OrderStatus.Canceled);
                order.CanceledTime.ShouldNotBeNull();
                order.CancellationReason.ShouldBe(OrdersConsts.UnpaidAutoCancellationReason);
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
            response.CancellationReason.ShouldBe(OrdersConsts.UnpaidAutoCancellationReason);

            UsingDbContext(db =>
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
                order.ShouldNotBeNull();
                order.PaymentId.ShouldBeNull();
                order.PaymentExpiration.ShouldBe(now);
                order.OrderStatus.ShouldBe(OrderStatus.Canceled);
                order.CanceledTime.ShouldNotBeNull();
                order.CancellationReason.ShouldBe(OrdersConsts.UnpaidAutoCancellationReason);
            });
        }

        [Fact]
        public async Task Should_Override_Unit_Price()
        {
            var createOrderDto = new CreateOrderDto
            {
                CustomerRemark = "customer remark",
                StoreId = OrderTestData.Store1Id,
                OrderLines = new List<CreateOrderLineDto>
                {
                    new()
                    {
                        ProductId = OrderTestData.Product1Id,
                        ProductSkuId = OrderTestData.ProductSku1Id,
                        Quantity = 10
                    },
                    new()
                    {
                        ProductId = OrderTestData.Product1Id,
                        ProductSkuId = OrderTestData.ProductSku3Id,
                        Quantity = 2
                    }
                }
            };

            await WithUnitOfWorkAsync(async () =>
            {
                var order = await _orderAppService.CreateAsync(createOrderDto);
                var orderLine = order.OrderLines.Find(x => x.ProductSkuId == OrderTestData.ProductSku3Id);

                order.ProductTotalPrice.ShouldBe(10 * 1m + 2 * TestOrderLinePriceOverrider.Sku3UnitPrice);
                orderLine.ShouldNotBeNull();
                orderLine.UnitPrice.ShouldBe(TestOrderLinePriceOverrider.Sku3UnitPrice);
                orderLine.TotalPrice.ShouldBe(orderLine.Quantity * orderLine.UnitPrice);
            });
        }

        [Fact]
        public async Task Should_Not_Create_Order_With_Flash_Sales_Products()
        {
            var createOrderDto = new CreateOrderDto
            {
                StoreId = OrderTestData.Store1Id,
                OrderLines = new List<CreateOrderLineDto>
                {
                    new()
                    {
                        ProductId = OrderTestData.Product1Id,
                        ProductSkuId = OrderTestData.ProductSku1Id,
                        Quantity = 1
                    }
                }
            };

            try
            {
                Product1.InventoryStrategy = InventoryStrategy.FlashSales;

                await WithUnitOfWorkAsync(async () =>
                {
                    var exception =
                        await Should.ThrowAsync<BusinessException>(() => _orderAppService.CreateAsync(createOrderDto));

                    exception.Code.ShouldBe(OrdersErrorCodes.ExistFlashSalesProduct);
                });

                Product1.InventoryStrategy = InventoryStrategy.NoNeed;
            }
            catch
            {
                Product1.InventoryStrategy = InventoryStrategy.NoNeed;
                throw;
            }
        }

        [Fact]
        public async Task Should_Throw_If_Product_Sku_Uses_Unexpected_Currency()
        {
            var createOrderDto = new CreateOrderDto
            {
                StoreId = OrderTestData.Store1Id,
                OrderLines = new List<CreateOrderLineDto>
                {
                    new()
                    {
                        ProductId = OrderTestData.Product1Id,
                        ProductSkuId = OrderTestData.ProductSku1Id,
                        Quantity = 1
                    }
                }
            };

            try
            {
                OrdersSettingDefinitionProvider.DefaultCurrency = "CNY"; // The effective value is "USD"
                await WithUnitOfWorkAsync(async () =>
                {
                    var exception =
                        await Should.ThrowAsync<BusinessException>(() => _orderAppService.CreateAsync(createOrderDto));

                    exception.Code.ShouldBe(OrdersErrorCodes.UnexpectedCurrency);
                });

                OrdersSettingDefinitionProvider.DefaultCurrency = "USD";
            }
            catch
            {
                OrdersSettingDefinitionProvider.DefaultCurrency = "USD";
                throw;
            }
        }
    }
}