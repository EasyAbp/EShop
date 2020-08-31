using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Payments.Payments;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class PaymentCreatedEventHandler : IPaymentCreatedEventHandler, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IOrderPaymentChecker _orderPaymentChecker;
        private readonly IOrderRepository _orderRepository;

        public PaymentCreatedEventHandler(
            ICurrentTenant currentTenant,
            IOrderPaymentChecker orderPaymentChecker,
            IOrderRepository orderRepository)
        {
            _currentTenant = currentTenant;
            _orderPaymentChecker = orderPaymentChecker;
            _orderRepository = orderRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityCreatedEto<EShopPaymentEto> eventData)
        {
            using var currentTenant = _currentTenant.Change(eventData.Entity.TenantId);

            foreach (var item in eventData.Entity.PaymentItems.Where(item => item.ItemType == PaymentsConsts.PaymentItemType))
            {
                var orderId = Guid.Parse(item.ItemKey);
                
                var order = await _orderRepository.GetAsync(orderId);

                if (order.PaymentId.HasValue || order.OrderStatus != OrderStatus.Pending)
                {
                    // Todo: should cancel the payment?
                    throw new OrderIsInWrongStageException(order.Id);
                }
                
                if (!await _orderPaymentChecker.IsValidPaymentAsync(order, eventData.Entity, item))
                {
                    // Todo: should cancel the payment?
                    throw new InvalidPaymentException(eventData.Entity.Id, orderId);
                }

                order.SetPaymentId(eventData.Entity.Id);

                await _orderRepository.UpdateAsync(order, true);
            }
        }
    }
}