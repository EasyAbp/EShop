using EasyAbp.EShop.Payments.Authorization;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using EasyAbp.EShop.Stores.Permissions;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.PaymentService.Payments;
using EasyAbp.PaymentService.Refunds;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Collections.Generic
using System.Threading.Tasks;
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
        
        protected override IQueryable<Refund> CreateFilteredQuery(GetRefundListDto input)
        {
            var query = input.UserId.HasValue ? _repository.GetQueryableByUserId(input.UserId.Value) : _repository;

            return query;
        }

        // Todo: should a store owner user see orders of other stores in the same payment/refund?
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

                // Todo: Check if current user is an admin of the store.

                foreach (var orderLineRefundInfoModel in refundItem.OrderLines)
                {
                    var orderLine = order.OrderLines.Single(x => x.Id == orderLineRefundInfoModel.OrderLineId);
                    
                    if (orderLine.RefundedQuantity + orderLineRefundInfoModel.Quantity > orderLine.Quantity)
                    {
                        throw new InvalidRefundQuantityException(orderLineRefundInfoModel.Quantity);
                    }
                }

                createRefundInput.RefundItems.Add(new CreateRefundItemInput
                {
                    PaymentItemId = refundItem.OrderId,
                    RefundAmount = refundItem.OrderLines.Sum(x => x.TotalAmount),
                    CustomerRemark = refundItem.CustomerRemark,
                    StaffRemark = refundItem.StaffRemark,
                    ExtraProperties = new Dictionary<string, object>
                    {
                        {"StoreId", order.StoreId.ToString()},
                        {"OrderId", order.Id.ToString()},
                        {"OrderLines", _jsonSerializer.Serialize(refundItem.OrderLines)}
                    }
                });
            }

            await _distributedEventBus.PublishAsync(new RefundPaymentEto
            {
                TenantId = CurrentTenant.Id,
                CreateRefundInput = createRefundInput
            });
        }
    }
}