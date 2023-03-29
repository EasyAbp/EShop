using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Payments;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.Core;
using Shouldly;
using Volo.Abp.Data;
using Volo.Abp.Json;
using Xunit;

namespace EasyAbp.EShop.Payments.Refunds;

public class RefundOrderEventHandlerTests : PaymentsDomainTestBase
{
    private readonly IJsonSerializer _jsonSerializer;

    public RefundOrderEventHandlerTests()
    {
        _jsonSerializer = ServiceProvider.GetRequiredService<IJsonSerializer>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        MockPaymentRepository(services);
    }

    private static void MockPaymentRepository(IServiceCollection services)
    {
        var paymentRepository = Substitute.For<IPaymentRepository>();

        Payment Payment1Returns(CallInfo _)
        {
            var paymentType = typeof(Payment);
            var paymentItemType = typeof(PaymentItem);

            var paymentItem = Activator.CreateInstance(paymentItemType, true) as PaymentItem;
            paymentItem.ShouldNotBeNull();
            paymentItemType.GetProperty(nameof(PaymentItem.Id))?.SetValue(paymentItem, PaymentsTestData.PaymentItem1);
            paymentItemType.GetProperty(nameof(PaymentItem.ActualPaymentAmount))?.SetValue(paymentItem, 1m);
            paymentItemType.GetProperty(nameof(PaymentItem.ItemType))
                ?.SetValue(paymentItem, PaymentsConsts.PaymentItemType);
            paymentItemType.GetProperty(nameof(PaymentItem.ItemKey))
                ?.SetValue(paymentItem, PaymentsTestData.Order1.ToString());
            paymentItemType.GetProperty(nameof(PaymentItem.StoreId))
                ?.SetValue(paymentItem, PaymentsTestData.Store1);

            var payment = Activator.CreateInstance(paymentType, true) as Payment;
            payment.ShouldNotBeNull();
            paymentType.GetProperty(nameof(Payment.Id))?.SetValue(payment, PaymentsTestData.Payment1);
            paymentType.GetProperty(nameof(Payment.Currency))?.SetValue(payment, "USD");
            paymentType.GetProperty(nameof(Payment.ActualPaymentAmount))?.SetValue(payment, 1m);
            paymentType.GetProperty(nameof(Payment.PaymentItems))
                ?.SetValue(payment, new List<PaymentItem> { paymentItem });

            return payment;
        }

        paymentRepository.GetAsync(PaymentsTestData.Payment1).Returns(Payment1Returns);
        paymentRepository.FindAsync(PaymentsTestData.Payment1).Returns(Payment1Returns);

        services.AddTransient(_ => paymentRepository);
    }

    [Fact]
    public async Task Should_Refund_Order()
    {
        var handler = ServiceProvider.GetRequiredService<RefundOrderEventHandler>();

        var eto = new RefundOrderEto(null, PaymentsTestData.Order1, PaymentsTestData.Store1,
            PaymentsTestData.Payment1, "Test", null, null);

        eto.OrderLines.Add(new OrderLineRefundInfoModel
        {
            OrderLineId = PaymentsTestData.OrderLine1,
            Quantity = 2,
            TotalAmount = 0.4m
        });

        eto.OrderExtraFees.Add(new OrderExtraFeeRefundInfoModel
        {
            Name = "Name",
            Key = "Key",
            DisplayName = "DisplayName",
            TotalAmount = 0.6m
        });

        await handler.HandleEventAsync(eto);

        var eventData = TestRefundPaymentEventHandler.LastEto;
        TestRefundPaymentEventHandler.LastEto = null;
        eventData.ShouldNotBeNull();
        eventData.CreateRefundInput.RefundItems.Count.ShouldBe(1);

        var refundItem = eventData.CreateRefundInput.RefundItems[0];
        refundItem.GetProperty<Guid?>(nameof(RefundItem.OrderId)).ShouldBe(PaymentsTestData.Order1);

        var orderLines =
            _jsonSerializer.Deserialize<List<OrderLineRefundInfoModel>>(
                refundItem.GetProperty<string>(nameof(RefundItem.OrderLines)));

        orderLines.Count.ShouldBe(1);
        orderLines[0].OrderLineId.ShouldBe(PaymentsTestData.OrderLine1);
        orderLines[0].Quantity.ShouldBe(2);
        orderLines[0].TotalAmount.ShouldBe(0.4m);

        var orderExtraFees =
            _jsonSerializer.Deserialize<List<OrderExtraFeeRefundInfoModel>>(
                refundItem.GetProperty<string>(nameof(RefundItem.OrderExtraFees)));

        orderExtraFees.Count.ShouldBe(1);
        orderExtraFees[0].Name.ShouldBe("Name");
        orderExtraFees[0].Key.ShouldBe("Key");
        orderExtraFees[0].DisplayName.ShouldBe("DisplayName");
        orderExtraFees[0].TotalAmount.ShouldBe(0.6m);
    }
}