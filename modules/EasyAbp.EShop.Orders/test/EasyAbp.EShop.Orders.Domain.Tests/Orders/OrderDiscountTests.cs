using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Orders.Orders;

public class OrderDiscountTests : OrdersDomainTestBase
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
                new(OrderTestData.Product1Id, OrderTestData.ProductSku1Id, 2)
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
                                Price = 1m,
                                Currency = "USD",
                                OrderMinQuantity = 1,
                                OrderMaxQuantity = 100,
                            }
                        }
                    }
                }
            }, new Dictionary<Guid, DateTime>());

        order.ActualTotalPrice.ShouldBe(1.89m);
        order.TotalDiscount.ShouldBe(0.11m);
        order.OrderDiscounts.Count.ShouldBe(2);
        order.OrderDiscounts.ShouldContain(x =>
            x.OrderId == order.Id && x.OrderLineId == order.OrderLines.First().Id && x.Name == "DemoDiscount1" &&
            x.Key == "1" && x.DisplayName == "Demo Discount 1" && x.DiscountedAmount == 0.01m);
        order.OrderDiscounts.ShouldContain(x =>
            x.OrderId == order.Id && x.OrderLineId == order.OrderLines.First().Id && x.Name == "DemoDiscount2" &&
            x.Key == "2" && x.DisplayName == "Demo Discount 2" && x.DiscountedAmount == 0.1m);
    }
}