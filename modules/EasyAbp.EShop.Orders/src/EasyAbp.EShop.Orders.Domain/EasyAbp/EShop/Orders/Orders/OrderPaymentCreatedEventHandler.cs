using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Payments;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderPaymentCreatedEventHandler : IOrderPaymentCreatedEventHandler, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IOrderRepository _orderRepository;

        public OrderPaymentCreatedEventHandler(
            ICurrentTenant currentTenant,
            IOrderRepository orderRepository)
        {
            _currentTenant = currentTenant;
            _orderRepository = orderRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityCreatedEto<PaymentEto> eventData)
        {
            using var currentTenant = _currentTenant.Change(eventData.Entity.TenantId);

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