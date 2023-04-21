using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.DependencyInjection;
using NodaMoney;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Orders.Orders;

public class OrderDiscountProviderTests : OrdersDomainTestBase
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.AddTransient<IOrderDiscountProvider, DemoOrderDiscountProvider>();
        base.AfterAddApplication(services);
    }

    [Fact]
    public async Task Should_Create_Order_With_Discount()
    {
        var orderGenerator = GetRequiredService<INewOrderGenerator>();

        var createOrderInfoModel = new CreateOrderInfoModel(OrderTestData.Store1Id, null,
            new List<CreateOrderLineInfoModel>
            {
                new(OrderTestData.Product1Id, OrderTestData.ProductSku1Id, 2),
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
                                Price = 100m,
                                Currency = "USD",
                                OrderMinQuantity = 1,
                                OrderMaxQuantity = 100,
                            },
                            new()
                            {
                                Id = OrderTestData.ProductSku2Id,
                                AttributeOptionIds = new List<Guid>(),
                                Price = 2m,
                                Currency = "USD",
                                OrderMinQuantity = 1,
                                OrderMaxQuantity = 100,
                            }
                        }
                    }
                }
            }, new Dictionary<Guid, DateTime>());

        var orderLines = order.OrderLines;

        const decimal orderLine1PriceWithoutDiscount = 2 * 100m;
        const decimal orderLine2PriceWithoutDiscount = 1 * 2m;

        var orderLine1ExpectedPrice = new Money((orderLine1PriceWithoutDiscount - 5m - 0.01m) * (1 - 0.55m), "USD",
            MidpointRounding.AwayFromZero).Amount;
        var orderLine2ExpectedPrice = orderLine2PriceWithoutDiscount - 0.1m;

        order.ActualTotalPrice.ShouldBe(orderLine1ExpectedPrice + orderLine2ExpectedPrice);
        order.TotalDiscount.ShouldBe(order.TotalPrice - order.ActualTotalPrice);
        order.OrderDiscounts.Count.ShouldBe(5);
        order.OrderDiscounts.ShouldContain(x =>
            x.OrderId == order.Id && x.OrderLineId == orderLines[0].Id && x.Name == "DemoDiscount1" &&
            x.Key == "1" && x.DisplayName == "Demo Discount 1" && x.DiscountedAmount == 5.00m);
        order.OrderDiscounts.ShouldContain(x =>
            x.OrderId == order.Id && x.OrderLineId == orderLines[1].Id && x.Name == "DemoDiscount2" &&
            x.Key == "2" && x.DisplayName == "Demo Discount 2" && x.DiscountedAmount == 0.10m);
        order.OrderDiscounts.ShouldContain(x =>
            x.OrderId == order.Id && x.OrderLineId == orderLines[0].Id && x.Name == "DemoDiscount5" &&
            x.Key == "5" && x.DisplayName == "Demo Discount 5" && x.DiscountedAmount == 0.01m);
        order.OrderDiscounts.ShouldContain(x =>
            x.OrderId == order.Id && x.OrderLineId == orderLines[1].Id && x.Name == "DemoDiscount5" &&
            x.Key == "5" && x.DisplayName == "Demo Discount 5" && x.DiscountedAmount == 0m);
        order.OrderDiscounts.ShouldContain(x =>
            x.OrderId == order.Id && x.OrderLineId == orderLines[0].Id && x.Name == "DemoDiscount6" &&
            x.Key == "6" && x.DisplayName == "Demo Discount 6" && x.DiscountedAmount == 107.24m);
    }
}