using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class ProductInventoryReductionEventHandler : IProductInventoryReductionEventHandler, ITransientDependency
    {
        private readonly IClock _clock;
        private readonly ICurrentTenant _currentTenant;
        private readonly IOrderRepository _orderRepository;

        public ProductInventoryReductionEventHandler(
            IClock clock,
            ICurrentTenant currentTenant,
            IOrderRepository orderRepository)
        {
            _clock = clock;
            _currentTenant = currentTenant;
            _orderRepository = orderRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(ProductInventoryReductionAfterOrderPlacedResultEto eventData)
        {
            using (_currentTenant.Change(eventData.TenantId))
            {
                var order = await _orderRepository.GetAsync(eventData.OrderId);

                if (order.ReducedInventoryAfterPlacingTime.HasValue)
                {
                    throw new OrderIsInWrongStageException(order.Id);
                }

                if (!eventData.IsSuccess)
                {
                    // Todo: Cancel order.
                    return;
                }
            
                order.SetReducedInventoryAfterPlacingTime(_clock.Now);

                await _orderRepository.UpdateAsync(order, true);
            }
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(ProductInventoryReductionAfterOrderPaidResultEto eventData)
        {
            using (_currentTenant.Change(eventData.TenantId))
            {
                var order = await _orderRepository.GetAsync(eventData.OrderId);

                if (order.ReducedInventoryAfterPaymentTime.HasValue)
                {
                    throw new OrderIsInWrongStageException(order.Id);
                }

                if (!eventData.IsSuccess)
                {
                    // Todo: Refund.
                    // Todo: Cancel order.
                    return;
                }
            
                order.SetReducedInventoryAfterPaymentTime(_clock.Now);

                await _orderRepository.UpdateAsync(order, true);
            }
        }
    }
}