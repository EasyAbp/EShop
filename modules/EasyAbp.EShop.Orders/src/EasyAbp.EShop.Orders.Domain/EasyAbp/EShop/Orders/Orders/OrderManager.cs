using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderManager : DomainService, IOrderManager
    {
        private readonly IClock _clock;
        private readonly IObjectMapper _objectMapper;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IOrderRepository _orderRepository;

        public OrderManager(
            IClock clock,
            IObjectMapper objectMapper,
            IDistributedEventBus distributedEventBus,
            IUnitOfWorkManager unitOfWorkManager,
            IOrderRepository orderRepository)
        {
            _clock = clock;
            _objectMapper = objectMapper;
            _distributedEventBus = distributedEventBus;
            _unitOfWorkManager = unitOfWorkManager;
            _orderRepository = orderRepository;
        }
        
        public virtual async Task<Order> DiscountAsync(Order order)
        {
            foreach (var provider in ServiceProvider.GetServices<IOrderDiscountProvider>())
            {
                await provider.DiscountAsync(order);
            }

            return order;
        }

        public virtual async Task<Order> CompleteAsync(Order order)
        {
            if (order.CompletionTime.HasValue || !order.ReducedInventoryAfterPaymentTime.HasValue)
            {
                throw new OrderIsInWrongStageException(order.Id);
            }
            
            var providers = ServiceProvider.GetServices<IOrderCompletableCheckProvider>();

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            
            foreach (var provider in providers)
            {
                await provider.CheckAsync(order);
            }
            
            order.SetCompletionTime(_clock.Now);
            order.SetOrderStatus(OrderStatus.Completed);

            uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new OrderCompletedEto
            {
                Order = _objectMapper.Map<Order, OrderEto>(order)
            }));

            await _orderRepository.UpdateAsync(order, true);

            await uow.CompleteAsync();
            
            return order;
        }

        public virtual async Task<Order> CancelAsync(Order order, string cancellationReason)
        {
            if (order.CanceledTime.HasValue)
            {
                throw new OrderIsInWrongStageException(order.Id);
            }
            
            if (order.IsInPayment())
            {
                throw new OrderIsInWrongStageException(order.Id);
            }

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            
            order.SetCanceled(_clock.Now, cancellationReason);
            order.SetOrderStatus(OrderStatus.Canceled);

            uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new OrderCanceledEto
            {
                Order = _objectMapper.Map<Order, OrderEto>(order)
            }));

            await _orderRepository.UpdateAsync(order, true);

            await uow.CompleteAsync();

            return order;
        }
    }
}