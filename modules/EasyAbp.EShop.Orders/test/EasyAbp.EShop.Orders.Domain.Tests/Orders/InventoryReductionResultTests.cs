using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Orders.Orders;

public class InventoryReductionResultTests : OrdersDomainTestBase
{
    private Order Order1 { get; set; }

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
            0.3m
        ));

        orderRepository.GetAsync(OrderTestData.Order1Id).Returns(Task.FromResult(Order1));

        services.AddTransient(_ => orderRepository);
    }

    [Fact]
    public async Task Should_Cancel_Order_If_Reduction_Failed_After_Placed()
    {
        typeof(Order).GetProperty(nameof(Order.CanceledTime))!.SetValue(Order1, null);
        typeof(Order).GetProperty(nameof(Order.CancellationReason))!.SetValue(Order1, null);
        Order1.SetReducedInventoryAfterPlacingTime(null);
        Order1.SetReducedInventoryAfterPaymentTime(null);
        Order1.SetOrderStatus(OrderStatus.Pending);
        Order1.SetPaymentId(null);
        Order1.SetPaidTime(null);

        var handler = ServiceProvider.GetRequiredService<ProductInventoryReductionEventHandler>();

        await handler.HandleEventAsync(new ProductInventoryReductionAfterOrderPlacedResultEto()
        {
            TenantId = null,
            OrderId = OrderTestData.Order1Id,
            IsSuccess = false
        });

        Order1.CanceledTime.ShouldNotBeNull();
        Order1.CancellationReason.ShouldBe(OrdersConsts.InventoryReductionFailedAutoCancellationReason);
        Order1.ReducedInventoryAfterPlacingTime.ShouldBeNull();
        Order1.ReducedInventoryAfterPaymentTime.ShouldBeNull();
    }

    [Fact]
    public async Task Should_Not_Cancel_Order_If_Reduction_Succeeded_After_Placed()
    {
        typeof(Order).GetProperty(nameof(Order.CanceledTime))!.SetValue(Order1, null);
        typeof(Order).GetProperty(nameof(Order.CancellationReason))!.SetValue(Order1, null);
        Order1.SetReducedInventoryAfterPlacingTime(null);
        Order1.SetReducedInventoryAfterPaymentTime(null);
        Order1.SetOrderStatus(OrderStatus.Pending);
        Order1.SetPaymentId(null);
        Order1.SetPaidTime(null);

        var handler = ServiceProvider.GetRequiredService<ProductInventoryReductionEventHandler>();

        await handler.HandleEventAsync(new ProductInventoryReductionAfterOrderPlacedResultEto()
        {
            TenantId = null,
            OrderId = OrderTestData.Order1Id,
            IsSuccess = true
        });

        Order1.CanceledTime.ShouldBeNull();
        Order1.CancellationReason.ShouldBeNull();
        Order1.ReducedInventoryAfterPlacingTime.ShouldNotBeNull();
        Order1.ReducedInventoryAfterPaymentTime.ShouldBeNull();
    }

    [Fact]
    public async Task Should_Cancel_Order_And_Refund_If_Reduction_Failed_After_Paid()
    {
        typeof(Order).GetProperty(nameof(Order.CanceledTime))!.SetValue(Order1, null);
        typeof(Order).GetProperty(nameof(Order.CancellationReason))!.SetValue(Order1, null);
        Order1.SetReducedInventoryAfterPlacingTime(DateTime.Now);
        Order1.SetReducedInventoryAfterPaymentTime(null);
        Order1.SetOrderStatus(OrderStatus.Processing);
        Order1.SetPaymentId(OrderTestData.Payment1Id);
        Order1.SetPaidTime(DateTime.Now);

        var handler = ServiceProvider.GetRequiredService<ProductInventoryReductionEventHandler>();

        await handler.HandleEventAsync(new ProductInventoryReductionAfterOrderPaidResultEto()
        {
            TenantId = null,
            OrderId = OrderTestData.Order1Id,
            IsSuccess = false
        });

        var eventData = TestRefundOrderEventHandler.LastEto;
        TestRefundOrderEventHandler.LastEto = null;
        eventData.ShouldNotBeNull();
        eventData.DisplayReason.ShouldBe(OrdersConsts.InventoryReductionFailedAutoCancellationReason);
        eventData.StaffRemark.ShouldBe(OrdersConsts.InventoryReductionFailedAutoCancellationReason);
        eventData.CustomerRemark.ShouldBe(OrdersConsts.InventoryReductionFailedAutoCancellationReason);
        eventData.PaymentId.ShouldBe(OrderTestData.Payment1Id);
        eventData.TenantId.ShouldBeNull();
        eventData.OrderId.ShouldBe(OrderTestData.Order1Id);

        eventData.OrderLines.Count.ShouldBe(1);
        var orderLine = eventData.OrderLines[0];
        orderLine.OrderLineId.ShouldBe(OrderTestData.OrderLine1Id);
        orderLine.Quantity.ShouldBe(2);
        orderLine.TotalAmount.ShouldBe(1m);

        eventData.OrderExtraFees.Count.ShouldBe(1);
        var orderExtraFee = eventData.OrderExtraFees[0];
        orderExtraFee.Name.ShouldBe("Name");
        orderExtraFee.Key.ShouldBe("Key");
        orderExtraFee.TotalAmount.ShouldBe(0.3m);

        Order1.CanceledTime.ShouldNotBeNull();
        Order1.CancellationReason.ShouldBe(OrdersConsts.InventoryReductionFailedAutoCancellationReason);
        Order1.ReducedInventoryAfterPlacingTime.ShouldNotBeNull();
        Order1.ReducedInventoryAfterPaymentTime.ShouldBeNull();
    }

    [Fact]
    public async Task Should_Not_Cancel_And_Refund_Order_If_Reduction_Succeeded_After_Paid()
    {
        typeof(Order).GetProperty(nameof(Order.CanceledTime))!.SetValue(Order1, null);
        typeof(Order).GetProperty(nameof(Order.CancellationReason))!.SetValue(Order1, null);
        Order1.SetReducedInventoryAfterPlacingTime(DateTime.Now);
        Order1.SetReducedInventoryAfterPaymentTime(null);
        Order1.SetOrderStatus(OrderStatus.Processing);
        Order1.SetPaymentId(OrderTestData.Payment1Id);
        Order1.SetPaidTime(DateTime.Now);

        var handler = ServiceProvider.GetRequiredService<ProductInventoryReductionEventHandler>();

        await handler.HandleEventAsync(new ProductInventoryReductionAfterOrderPaidResultEto()
        {
            TenantId = null,
            OrderId = OrderTestData.Order1Id,
            IsSuccess = true
        });

        var eventData = TestRefundOrderEventHandler.LastEto;
        TestRefundOrderEventHandler.LastEto = null;
        eventData.ShouldBeNull();

        Order1.CanceledTime.ShouldBeNull();
        Order1.CancellationReason.ShouldBeNull();
        Order1.ReducedInventoryAfterPlacingTime.ShouldNotBeNull();
        Order1.ReducedInventoryAfterPaymentTime.ShouldNotBeNull();
    }
}