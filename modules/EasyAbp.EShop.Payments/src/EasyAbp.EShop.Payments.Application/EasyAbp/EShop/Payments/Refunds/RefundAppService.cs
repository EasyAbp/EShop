using EasyAbp.EShop.Payments.Authorization;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.PaymentService.Payments;
using EasyAbp.PaymentService.Refunds;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Json;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Payments.Refunds
{
    [Authorize]
    public class RefundAppService : ReadOnlyAppService<Refund, RefundDto, Guid, GetRefundListDto>,
        IRefundAppService
    {
        protected override string GetPolicyName { get; set; } = PaymentsPermissions.Refunds.Manage;
        protected override string GetListPolicyName { get; set; } = PaymentsPermissions.Refunds.Manage;

        private readonly IOrderAppService _orderAppService;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IRefundRepository _repository;

        public RefundAppService(
            IOrderAppService orderAppService,
            IDistributedEventBus distributedEventBus,
            IPaymentRepository paymentRepository,
            IJsonSerializer jsonSerializer,
            IRefundRepository repository) : base(repository)
        {
            _orderAppService = orderAppService;
            _distributedEventBus = distributedEventBus;
            _paymentRepository = paymentRepository;
            _jsonSerializer = jsonSerializer;
            _repository = repository;
        }

        // Todo: should a store owner user see orders of other stores in the same payment/refund?
        public override async Task<RefundDto> GetAsync(Guid id)
        {
            var refund = await base.GetAsync(id);

            var payment = await _paymentRepository.GetAsync(refund.PaymentId);

            if (payment.UserId != CurrentUser.GetId())
            {
                await CheckPolicyAsync(GetPolicyName);
            }

            return refund;
        }

        protected override async Task<IQueryable<Refund>> CreateFilteredQueryAsync(GetRefundListDto input)
        {
            return input.UserId.HasValue
                ? await _repository.GetQueryableByUserIdAsync(input.UserId.Value)
                : await _repository.GetQueryableAsync();
        }

        // Todo: should a store owner user see orders of other stores in the same payment/refund?
        [Authorize]
        public override async Task<PagedResultDto<RefundDto>> GetListAsync(GetRefundListDto input)
        {
            if (input.UserId != CurrentUser.GetId())
            {
                await CheckPolicyAsync(GetListPolicyName);
            }

            return await base.GetListAsync(input);
        }

        public virtual async Task CreateAsync(CreateEShopRefundInput input)
        {
            await AuthorizationService.CheckAsync(PaymentsPermissions.Refunds.Manage);

            // todo: needs a lock.
            var payment = await _paymentRepository.GetAsync(input.PaymentId);

            if (payment.PendingRefundAmount != decimal.Zero)
            {
                throw new AnotherRefundTaskIsOnGoingException(payment.Id);
            }

            var createRefundInput = new CreateRefundInput
            {
                PaymentId = input.PaymentId,
                DisplayReason = input.DisplayReason,
                CustomerRemark = input.CustomerRemark,
                StaffRemark = input.StaffRemark
            };

            foreach (var refundItem in input.RefundItems)
            {
                var order = await _orderAppService.GetAsync(refundItem.OrderId);

                var paymentItem = payment.PaymentItems.SingleOrDefault(x => x.ItemKey == refundItem.OrderId.ToString());

                if (order.PaymentId != input.PaymentId || paymentItem == null)
                {
                    throw new OrderIsNotInSpecifiedPaymentException(order.Id, payment.Id);
                }

                await AuthorizationService.CheckMultiStorePolicyAsync(paymentItem.StoreId,
                    PaymentsPermissions.Refunds.Manage, PaymentsPermissions.Refunds.CrossStore);

                var refundAmount = refundItem.OrderLines.Sum(x => x.TotalAmount) +
                                   refundItem.OrderExtraFees.Sum(x => x.TotalAmount);

                if (refundAmount + paymentItem.RefundAmount > paymentItem.ActualPaymentAmount)
                {
                    throw new InvalidOrderRefundAmountException(payment.Id, paymentItem.Id, refundAmount);
                }

                foreach (var model in refundItem.OrderLines)
                {
                    var orderLine = order.OrderLines.Find(x => x.Id == model.OrderLineId);

                    if (orderLine is null)
                    {
                        throw new OrderLineNotFoundException(order.Id, model.OrderLineId);
                    }

                    // PaymentAmount is always null before EShop v5
                    var paymentAmount = orderLine.PaymentAmount ?? orderLine.ActualTotalPrice;
                    if (orderLine.RefundAmount + model.TotalAmount > paymentAmount)
                    {
                        throw new InvalidOrderLineRefundAmountException(
                            payment.Id, paymentItem.Id, orderLine.Id, refundAmount);
                    }

                    if (orderLine.RefundedQuantity + model.Quantity > orderLine.Quantity)
                    {
                        throw new InvalidRefundQuantityException(model.Quantity);
                    }
                }

                foreach (var model in refundItem.OrderExtraFees)
                {
                    var orderExtraFee = order.OrderExtraFees.Find(x => x.Name == model.Name && x.Key == model.Key);

                    if (orderExtraFee is null)
                    {
                        throw new OrderExtraFeeNotFoundException(order.Id, model.Name, model.Key);
                    }

                    // PaymentAmount is always null before EShop v5
                    var paymentAmount = orderExtraFee.PaymentAmount ?? orderExtraFee.Fee;
                    if (orderExtraFee.RefundAmount + model.TotalAmount > paymentAmount)
                    {
                        throw new InvalidOrderExtraFeeRefundAmountException(
                            payment.Id, paymentItem.Id, orderExtraFee.DisplayName, refundAmount);
                    }
                }

                var eto = new CreateRefundItemInput
                {
                    PaymentItemId = paymentItem.Id,
                    RefundAmount = refundAmount,
                    CustomerRemark = refundItem.CustomerRemark,
                    StaffRemark = refundItem.StaffRemark
                };

                eto.SetProperty(nameof(RefundItem.StoreId), order.StoreId);
                eto.SetProperty(nameof(RefundItem.OrderId), order.Id);
                eto.SetProperty(nameof(RefundItem.OrderLines), _jsonSerializer.Serialize(refundItem.OrderLines));
                eto.SetProperty(nameof(RefundItem.OrderExtraFees),
                    _jsonSerializer.Serialize(refundItem.OrderExtraFees));

                createRefundInput.RefundItems.Add(eto);
            }

            await _distributedEventBus.PublishAsync(new RefundPaymentEto(CurrentTenant.Id, createRefundInput));
        }
    }
}