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
            IObjectMapper<EShopOrdersDomainModule> objectMapper,
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

        [UnitOfWork]
        public virtual async Task<Order> CompleteAsync(Order order)
        {
            if (order.CompletionTime.HasValue)
            {
                throw new OrderIsInWrongStageException(order.Id);
            }
            
            var providers = LazyServiceProvider.LazyGetService<IEnumerable<IOrderCompletableCheckProvider>>();

            foreach (var provider in providers)
            {
                await provider.CheckAsync(order);
            }
            
            order.Complete(_clock.Now);

            await _orderRepository.UpdateAsync(order, true);

            await _distributedEventBus.PublishAsync(new OrderCompletedEto(_objectMapper.Map<Order, OrderEto>(order)));

            return order;
        }

        [UnitOfWork]
        public virtual async Task<Order> CancelAsync(Order order, string cancellationReason, bool forceCancel = false)
        {
            if (order.IsCanceled())
            {
                throw new OrderIsInWrongStageException(order.Id);
            }
            
            if (!forceCancel && (order.IsInPayment() || order.IsInInventoryDeductionStage()))
            {
                throw new OrderIsInWrongStageException(order.Id);
            }

            order.SetCanceled(_clock.Now, cancellationReason, forceCancel);
            
            await _orderRepository.UpdateAsync(order, true);
            
            await _distributedEventBus.PublishAsync(new OrderCanceledEto(_objectMapper.Map<Order, OrderEto>(order)));

            return order;
        }
    }
}