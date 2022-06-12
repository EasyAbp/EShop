using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.PaymentService.Payments;
using EasyAbp.PaymentService.Refunds;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Json;

namespace EasyAbp.EShop.Payments.Refunds;

public class RefundOrderEventHandler : IDistributedEventHandler<RefundOrderEto>, ITransientDependency
{
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IDistributedEventBus _distributedEventBus;

    public RefundOrderEventHandler(
        IJsonSerializer jsonSerializer,
        IPaymentRepository paymentRepository,
        IDistributedEventBus distributedEventBus)
    {
        _jsonSerializer = jsonSerializer;
        _paymentRepository = paymentRepository;
        _distributedEventBus = distributedEventBus;
    }

    public virtual async Task HandleEventAsync(RefundOrderEto eventData)
    {
        var refundAmount = eventData.OrderLines.Sum(x => x.TotalAmount) +
                           eventData.OrderExtraFees.Sum(x => x.TotalAmount);

        var payment = await _paymentRepository.GetAsync(eventData.PaymentId);

        var paymentItem = payment.PaymentItems.Single(x => x.ItemKey == eventData.OrderId.ToString());

        var createRefundItemInput = new CreateRefundItemInput
        {
            PaymentItemId = paymentItem.Id,
            RefundAmount = refundAmount,
            CustomerRemark = eventData.CustomerRemark,
            StaffRemark = eventData.StaffRemark
        };

        createRefundItemInput.SetProperty(nameof(RefundItem.StoreId), eventData.StoreId);
        createRefundItemInput.SetProperty(nameof(RefundItem.OrderId), eventData.OrderId);
        createRefundItemInput.SetProperty(nameof(RefundItem.OrderLines),
            _jsonSerializer.Serialize(eventData.OrderLines));
        createRefundItemInput.SetProperty(nameof(RefundItem.OrderExtraFees),
            _jsonSerializer.Serialize(eventData.OrderExtraFees));

        var eto = new RefundPaymentEto(eventData.TenantId, new CreateRefundInput
        {
            PaymentId = eventData.PaymentId,
            DisplayReason = eventData.DisplayReason,
            CustomerRemark = eventData.CustomerRemark,
            StaffRemark = eventData.StaffRemark,
            RefundItems = new List<CreateRefundItemInput> { createRefundItemInput }
        });

        await _distributedEventBus.PublishAsync(eto);
    }
}