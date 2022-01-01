using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Payments.Payments;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class PaymentCanceledEventHandler : IDistributedEventHandler<EShopPaymentCanceledEto>, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IOrderRepository _orderRepository;

        public PaymentCanceledEventHandler(
            ICurrentTenant currentTenant,
            IOrderRepository orderRepository)
        {
            _currentTenant = currentTenant;
            _orderRepository = orderRepository;
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EShopPaymentCanceledEto eventData)
        {
            using var currentTenant = _currentTenant.Change(eventData.TenantId);

            foreach (var paymentItem in eventData.Payment.PaymentItems.Where(item =>
                item.ItemType == PaymentsConsts.PaymentItemType))
            {
                var order = await _orderRepository.GetAsync(Guid.Parse(paymentItem.ItemKey));

                if (order.PaymentId != eventData.Payment.Id)
                {
                    continue;
                }
                
                order.SetPaymentId(null);

                await _orderRepository.UpdateAsync(order, true);
            }
        }
    }
}