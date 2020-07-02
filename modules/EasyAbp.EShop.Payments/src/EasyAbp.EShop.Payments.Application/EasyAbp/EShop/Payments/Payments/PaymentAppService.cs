using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Payments.Authorization;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Payments.Dtos;
using EasyAbp.PaymentService.Payments;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Payments.Payments
{
    [Authorize]
    public class PaymentAppService : ReadOnlyAppService<Payment, PaymentDto, Guid, GetPaymentListDto>,
        IPaymentAppService
    {
        protected override string GetPolicyName { get; set; } = PaymentsPermissions.Payments.Default;
        protected override string GetListPolicyName { get; set; } = PaymentsPermissions.Payments.Default;

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

        public override async Task<PaymentDto> GetAsync(Guid id)
        {
            var payment = await base.GetAsync(id);

            if (payment.UserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckAsync(PaymentsPermissions.Payments.Manage);

                // Todo: Check if current user is an admin of the store.
            }

            return payment;
        }
        
        protected override IQueryable<Payment> CreateFilteredQuery(GetPaymentListDto input)
        {
            var query = base.CreateFilteredQuery(input);

            if (input.StoreId.HasValue)
            {
                query = query.Where(x => x.StoreId == input.StoreId.Value);
            }

            return query;
        }

        public override async Task<PagedResultDto<PaymentDto>> GetListAsync(GetPaymentListDto input)
        {
            if (input.UserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckAsync(PaymentsPermissions.Payments.Manage);

                if (input.StoreId.HasValue)
                {
                    // Todo: Check if current user is an admin of the store.
                }
                else
                {
                    await AuthorizationService.CheckAsync(PaymentsPermissions.Payments.CrossStore);
                }
            }

            return await base.GetListAsync(input);
        }
        
        [Authorize(PaymentsPermissions.Payments.Create)]
        public async Task CreateAsync(CreatePaymentDto input)
        {
            var orders = new List<OrderDto>();
            
            foreach (var orderId in input.OrderIds)
            {
                var order = await _orderAppService.GetAsync(orderId);
                
                orders.Add(order);

                if (order.PaymentId.HasValue || order.PaidTime.HasValue)
                {
                    throw new OrderPaymentAlreadyExistsException(orderId);
                }
            }

            if (orders.Select(order => order.Currency).Distinct().Count() != 1)
            {
                throw new MultiCurrencyNotSupportedException();
            }
            
            if (orders.Select(order => order.StoreId).Distinct().Count() != 1)
            {
                throw new MultiStorePaymentNotSupportedException();
            }

            // Todo: should avoid duplicate creations.

            var extraProperties = new Dictionary<string, object> {{"StoreId", orders.First().StoreId}};

            await _distributedEventBus.PublishAsync(new CreatePaymentEto
            {
                TenantId = CurrentTenant.Id,
                UserId = CurrentUser.GetId(),
                PaymentMethod = input.PaymentMethod,
                Currency = orders.First().Currency,
                ExtraProperties = extraProperties,
                PaymentItems = orders.Select(order => new CreatePaymentItemEto
                {
                    ItemType = PaymentsConsts.PaymentItemType,
                    ItemKey = order.Id,
                    Currency = order.Currency,
                    OriginalPaymentAmount = order.TotalPrice
                }).ToList()
            });
        }
    }
}