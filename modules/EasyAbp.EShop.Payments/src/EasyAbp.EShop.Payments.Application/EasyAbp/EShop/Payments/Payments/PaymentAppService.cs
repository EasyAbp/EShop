using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Payments.Authorization;
using EasyAbp.EShop.Payments.Payments.Dtos;
using EasyAbp.PaymentService.Payments;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Payments.Payments
{
    [Authorize]
    public class PaymentAppService : ReadOnlyAppService<Payment, PaymentDto, Guid, GetPaymentListDto>,
        IPaymentAppService
    {
        protected override string GetPolicyName { get; set; } = PaymentsPermissions.Payments.Manage;
        protected override string GetListPolicyName { get; set; } = PaymentsPermissions.Payments.Manage;

        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IOrderAppService _orderAppService;
        private readonly IPaymentRepository _repository;
        
        public PaymentAppService(
            IDistributedEventBus distributedEventBus,
            IOrderAppService orderAppService,
            IPaymentRepository repository) : base(repository)
        {
            _distributedEventBus = distributedEventBus;
            _orderAppService = orderAppService;
            _repository = repository;
        }

        // Todo: should a store owner user see orders of other stores in the same payment/refund?
        public override async Task<PaymentDto> GetAsync(Guid id)
        {
            var payment = await base.GetAsync(id);

            if (payment.UserId != CurrentUser.GetId())
            {
                await CheckPolicyAsync(GetPolicyName);
            }

            return payment;
        }
        
        protected override IQueryable<Payment> CreateFilteredQuery(GetPaymentListDto input)
        {
            var query = base.CreateFilteredQuery(input);

            if (input.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == input.UserId.Value);
            }

            return query;
        }

        // Todo: should a store owner user see orders of other stores in the same payment/refund?
        [Authorize]
        public override async Task<PagedResultDto<PaymentDto>> GetListAsync(GetPaymentListDto input)
        {
            if (input.UserId != CurrentUser.GetId())
            {
                await CheckPolicyAsync(GetListPolicyName);
            }

            return await base.GetListAsync(input);
        }
        
        [Authorize(PaymentsPermissions.Payments.Create)]
        public virtual async Task CreateAsync(CreatePaymentDto input)
        {
            // Todo: should avoid duplicate creations. (concurrent lock)

            var orders = new List<OrderDto>();
            
            foreach (var orderId in input.OrderIds)
            {
                orders.Add(await _orderAppService.GetAsync(orderId));
            }
            
            await AuthorizationService.CheckAsync(
                new PaymentCreationResource
                {
                    Input = input,
                    Orders = orders
                },
                new PaymentOperationAuthorizationRequirement(PaymentOperation.Creation)
            );

            var createPaymentEto = new CreatePaymentEto
            {
                TenantId = CurrentTenant.Id,
                UserId = CurrentUser.GetId(),
                PaymentMethod = input.PaymentMethod,
                Currency = orders.First().Currency,
                ExtraProperties = new ExtraPropertyDictionary(),
                PaymentItems = orders.Select(order => new CreatePaymentItemEto
                {
                    ItemType = PaymentsConsts.PaymentItemType,
                    ItemKey = order.Id.ToString(),
                    OriginalPaymentAmount = order.ActualTotalPrice,
                    ExtraProperties = new ExtraPropertyDictionary {{"StoreId", order.StoreId.ToString()}}
                }).ToList()
            };

            await _distributedEventBus.PublishAsync(createPaymentEto);
        }
    }
}