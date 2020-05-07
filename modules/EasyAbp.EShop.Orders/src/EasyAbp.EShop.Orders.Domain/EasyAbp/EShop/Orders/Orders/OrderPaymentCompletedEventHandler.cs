using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Payments;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderPaymentCompletedEventHandler : IOrderPaymentCompletedEventHandler, ITransientDependency
    {
        private readonly IClock _clock;
        private readonly IOrderRepository _orderRepository;

        public OrderPaymentCompletedEventHandler(
            IClock clock,
            IOrderRepository orderRepository)
        {
            _clock = clock;
            _orderRepository = orderRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityUpdatedEto<PaymentEto> eventData)
        {
            if (!eventData.Entity.CompletionTime.HasValue)
            {
                return;
            }

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