using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain.Services;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderManager : DomainService, IOrderManager
    {
        private readonly IClock _clock;
        private readonly IOrderRepository _orderRepository;

        public OrderManager(
            IClock clock,
            IOrderRepository orderRepository)
        {
            _clock = clock;
            _orderRepository = orderRepository;
        }
        
        public virtual async Task<Order> DiscountAsync(Order order, Dictionary<string, object> inputExtraProperties)
        {
            foreach (var provider in ServiceProvider.GetServices<IOrderDiscountProvider>())
            {
                await provider.DiscountAsync(order, inputExtraProperties);
            }

            return order;
        }

        public virtual async Task<Order> CompleteAsync(Order order)
        {
            if (order.CompletionTime.HasValue)
            {
                throw new OrderIsInWrongStageException(order.Id);
            }
            
            var providers = ServiceProvider.GetServices<ICompletableCheckProvider>();

            foreach (var provider in providers)
            {
                await provider.CheckAsync(order);
            }
            
            order.SetCompletionTime(_clock.Now);
            order.SetOrderStatus(OrderStatus.Completed);

            return await _orderRepository.UpdateAsync(order, true);
        }

        public virtual async Task<Order> CancelAsync(Order order, string cancellationReason)
        {
            order.Cancel(_clock.Now, cancellationReason);
            order.SetOrderStatus(OrderStatus.Canceled);

            return await _orderRepository.UpdateAsync(order, true);
        }
    }
}