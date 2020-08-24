using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderPaymentCompletedEventHandler : IOrderPaymentCompletedEventHandler, ITransientDependency
    {
        private readonly IClock _clock;
        private readonly ICurrentTenant _currentTenant;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IOrderPaymentChecker _orderPaymentChecker;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IOrderRepository _orderRepository;

        public OrderPaymentCompletedEventHandler(
            IClock clock,
            ICurrentTenant currentTenant,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWorkManager,
            IOrderPaymentChecker orderPaymentChecker,
            IDistributedEventBus distributedEventBus,
            IOrderRepository orderRepository)
        {
            _clock = clock;
            _currentTenant = currentTenant;
            _objectMapper = objectMapper;
            _unitOfWorkManager = unitOfWorkManager;
            _orderPaymentChecker = orderPaymentChecker;
            _distributedEventBus = distributedEventBus;
            _orderRepository = orderRepository;
        }
        
        public virtual async Task HandleEventAsync(PaymentCompletedEto eventData)
        {
            var payment = eventData.Payment;
            
            if (!payment.CompletionTime.HasValue || payment.CanceledTime.HasValue)
            {
                return;
            }

            using var currentTenant = _currentTenant.Change(payment.TenantId);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            foreach (var item in payment.PaymentItems.Where(item => item.ItemType == PaymentsConsts.PaymentItemType))
            {
                var orderId = Guid.Parse(item.ItemKey);
                
                var order = await _orderRepository.GetAsync(orderId);

                if (order.PaymentId != payment.Id || order.PaidTime.HasValue ||
                    order.OrderStatus != OrderStatus.Pending)
                {
                    throw new OrderIsInWrongStageException(order.Id);
                }

                if (!await _orderPaymentChecker.IsValidPaymentAsync(order, payment, item))
                {
                    throw new OrderPaymentInvalidException(payment.Id, orderId);
                }
                
                order.SetPaidTime(_clock.Now);
                order.SetOrderStatus(OrderStatus.Processing);

                await _orderRepository.UpdateAsync(order, true);

                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new OrderPaidEto
                {
                    Order = _objectMapper.Map<Order, OrderEto>(order),
                    PaymentId = payment.Id,
                    PaymentItemId = item.Id
                }));
            }
            
            await uow.CompleteAsync();
        }
    }
}