using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderProductInventoryReductionEventHandler : IOrderProductInventoryReductionEventHandler, ITransientDependency
    {
        private readonly IClock _clock;
        private readonly IOrderRepository _orderRepository;

        public OrderProductInventoryReductionEventHandler(
            IClock clock,
            IOrderRepository orderRepository)
        {
            _clock = clock;
            _orderRepository = orderRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(ProductInventoryReductionAfterOrderPlacedResultEto eventData)
        {
            var order = await _orderRepository.GetAsync(eventData.OrderId);

            if (order.OrderStatus != OrderStatus.Pending || order.ReducedInventoryAfterPlacingTime.HasValue)
            {
                return;
            }

            if (!eventData.IsSuccess)
            {
                // Todo: Cancel order.
                return;
            }
            
            order.SetReducedInventoryAfterPaymentTime(_clock.Now);

            await _orderRepository.UpdateAsync(order, true);
        }
    }
}