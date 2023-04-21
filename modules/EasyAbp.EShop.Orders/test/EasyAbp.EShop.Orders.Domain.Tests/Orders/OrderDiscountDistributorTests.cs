using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Orders.Orders;

public class OrderDiscountDistributorTests : OrdersDomainTestBase
{
    private IOrderDiscountDistributor OrderDiscountDistributor { get; }

    public OrderDiscountDistributorTests()
    {
        OrderDiscountDistributor = GetRequiredService<IOrderDiscountDistributor>();
    }

    [Fact]
    public async Task Should_Fall_Back_Discount_Amount_Using_MidpointRounding_ToZero()
    {
        var orderGenerator = GetRequiredService<INewOrderGenerator>();

        var createOrderInfoModel = new CreateOrderInfoModel(OrderTestData.Store1Id, null,
            new List<CreateOrderLineInfoModel>
            {
                new(OrderTestData.Product1Id, OrderTestData.ProductSku1Id, 1),
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
                                Price = 0.99m,
                                Currency = "USD",
                                OrderMinQuantity = 1,
                                OrderMaxQuantity = 100,
                            }
                        }
                    }
                }
            }, new Dictionary<Guid, DateTime>());

        const decimal discountRate = 0.50m;

        var discount = new OrderDiscountInfoModel(order.OrderLines.Select(x => x.Id).ToList(), null, "Test", null,
            null, new DynamicDiscountAmountModel("USD", 0m, discountRate, null));

        var currentTotalPrices =
            new Dictionary<IOrderLine, decimal>(order.OrderLines.ToDictionary(x => (IOrderLine)x,
                x => x.TotalPrice));

        var distributionResult = await OrderDiscountDistributor.DistributeAsync(order, currentTotalPrices, discount);

        order.AddDiscounts(distributionResult);

        var orderLine1 = order.OrderLines[0];
        var discountedAmount1 = Math.Round(orderLine1.TotalPrice * discountRate, 2, MidpointRounding.ToZero);

        discountedAmount1.ShouldBe(0.49m);

        var discount1 = order.OrderDiscounts.Find(x => x.OrderLineId == orderLine1.Id);

        order.OrderDiscounts.Count.ShouldBe(1);
        discount1.ShouldNotBeNull();

        discount1.DiscountedAmount.ShouldBe(discountedAmount1);
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

        var discount = new OrderDiscountInfoModel(order.OrderLines.Select(x => x.Id).ToList(), null, "Test", null,
            null, new DynamicDiscountAmountModel("USD", discountedAmount, 0m, null));

        var currentTotalPrices =
            new Dictionary<IOrderLine, decimal>(order.OrderLines.ToDictionary(x => (IOrderLine)x,
                x => x.TotalPrice));

        var distributionResult = await OrderDiscountDistributor.DistributeAsync(order, currentTotalPrices, discount);

        order.AddDiscounts(distributionResult);

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