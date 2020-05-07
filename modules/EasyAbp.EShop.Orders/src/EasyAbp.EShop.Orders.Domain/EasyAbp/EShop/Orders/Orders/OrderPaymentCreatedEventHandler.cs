using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Payments;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderPaymentCreatedEventHandler : IOrderPaymentCreatedEventHandler, ITransientDependency
    {
        private readonly IOrderRepository _orderRepository;

        public OrderPaymentCreatedEventHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityCreatedEto<PaymentEto> eventData)
        {
            foreach (var item in eventData.Entity.PaymentItems.Where(item => item.ItemType == "EasyAbpEShopOrder"))
            {
                var order = await _orderRepository.FindAsync(item.ItemKey);

                if (order == null || order.PaymentId.HasValue)
                {
                    continue;
                }
                
                order.SetPaymentId(eventData.Entity.Id);

                await _orderRepository.UpdateAsync(order, true);
            }
        }
    }
}