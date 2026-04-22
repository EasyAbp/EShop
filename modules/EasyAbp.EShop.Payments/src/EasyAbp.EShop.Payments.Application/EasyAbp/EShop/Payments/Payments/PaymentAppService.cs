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
using EasyAbp.EShop.Payments.Localization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Payments.Payments
{
    [Authorize]
    public class PaymentAppService : ReadOnlyAppService<Payment, PaymentDto, Guid, GetPaymentListDto>,
        IPaymentAppService
    {
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = null;

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

            LocalizationResource = typeof(PaymentsResource);
            ObjectMapperContext = typeof(EShopPaymentsApplicationModule);
        }

        // Todo: should a store owner user see orders of other stores in the same payment/refund?
        public override async Task<PaymentDto> GetAsync(Guid id)
        {
            var payment = await base.GetAsync(id);

            if (payment.UserId != CurrentUser.GetId())
            {
                await CheckPolicyAsync(PaymentsPermissions.Payments.Manage);
            }

            return payment;
        }

        protected override async Task<IQueryable<Payment>> CreateFilteredQueryAsync(GetPaymentListDto input)
        {
            var query = await base.CreateFilteredQueryAsync(input);

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
                await CheckPolicyAsync(PaymentsPermissions.Payments.Manage);
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

            var paymentItems = orders.Select(order =>
            {
                var eto = new CreatePaymentItemEto
                {
                    ItemType = PaymentsConsts.PaymentItemType,
                    ItemKey = order.Id.ToString(),
                    OriginalPaymentAmount = order.ActualTotalPrice
                };
                
                eto.SetProperty(nameof(PaymentItem.StoreId), order.StoreId);

                return eto;
            }).ToList();
            
            var createPaymentEto = new CreatePaymentEto(
                CurrentTenant.Id,
                CurrentUser.GetId(),
                input.PaymentMethod,
                orders.First().Currency,
                paymentItems
            );
            
            input.MapExtraPropertiesTo(createPaymentEto);

            await _distributedEventBus.PublishAsync(createPaymentEto);
        }
    }
}