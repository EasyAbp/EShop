using System.Linq;
using System.Threading.Tasks;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderPaymentCompletedEventHandler : IOrderPaymentCompletedEventHandler, ITransientDependency
    {
        private readonly IClock _clock;
        private readonly ICurrentTenant _currentTenant;
        private readonly IOrderRepository _orderRepository;

        public OrderPaymentCompletedEventHandler(
            IClock clock,
            ICurrentTenant currentTenant,
            IOrderRepository orderRepository)
        {
            _clock = clock;
            _currentTenant = currentTenant;
            _orderRepository = orderRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityUpdatedEto<PaymentEto> eventData)
        {
            if (!eventData.Entity.CompletionTime.HasValue)
            {
                return;
            }

            using var currentTenant = _currentTenant.Change(eventData.Entity.TenantId);

            foreach (var item in eventData.Entity.PaymentItems.Where(item => item.ItemType == "EasyAbpEShopOrder"))
            {
                var order = await _orderRepository.FindAsync(item.ItemKey);

                if (order == null || order.PaidTime.HasValue)
                {
                    continue;
                }
                
                order.SetPaidTime(_clock.Now);
                order.SetOrderStatus(OrderStatus.Processing);

                await _orderRepository.UpdateAsync(order, true);
            }
        }
    }
}