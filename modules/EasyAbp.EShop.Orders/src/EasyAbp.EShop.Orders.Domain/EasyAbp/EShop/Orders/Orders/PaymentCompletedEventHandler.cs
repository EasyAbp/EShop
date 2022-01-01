using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Payments.Payments;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class PaymentCompletedEventHandler : IPaymentCompletedEventHandler, ITransientDependency
    {
        private readonly IClock _clock;
        private readonly ICurrentTenant _currentTenant;
        private readonly IObjectMapper _objectMapper;
        private readonly IOrderPaymentChecker _orderPaymentChecker;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IOrderRepository _orderRepository;

        public PaymentCompletedEventHandler(
            IClock clock,
            ICurrentTenant currentTenant,
            IObjectMapper objectMapper,
            IOrderPaymentChecker orderPaymentChecker,
            IDistributedEventBus distributedEventBus,
            IOrderRepository orderRepository)
        {
            _clock = clock;
            _currentTenant = currentTenant;
            _objectMapper = objectMapper;
            _orderPaymentChecker = orderPaymentChecker;
            _distributedEventBus = distributedEventBus;
            _orderRepository = orderRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EShopPaymentCompletedEto eventData)
        {
            var payment = eventData.Payment;
            
            if (!payment.CompletionTime.HasValue || payment.CanceledTime.HasValue)
            {
                return;
            }

            using var currentTenant = _currentTenant.Change(payment.TenantId);

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
                    throw new InvalidPaymentException(payment.Id, orderId);
                }
                
                order.SetPaidTime(_clock.Now);
                order.SetOrderStatus(OrderStatus.Processing);

                await _orderRepository.UpdateAsync(order, true);

                await _distributedEventBus.PublishAsync(new OrderPaidEto(_objectMapper.Map<Order, OrderEto>(order),
                    payment.Id, item.Id));
            }
        }
    }
}