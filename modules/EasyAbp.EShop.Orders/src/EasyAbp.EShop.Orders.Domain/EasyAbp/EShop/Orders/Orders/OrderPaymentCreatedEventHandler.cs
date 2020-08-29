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
    public class EShopPaymentCreatedEventHandler : IEShopPaymentCreatedEventHandler, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IEShopPaymentChecker _eShopPaymentChecker;
        private readonly IOrderRepository _orderRepository;

        public EShopPaymentCreatedEventHandler(
            ICurrentTenant currentTenant,
            IEShopPaymentChecker eShopPaymentChecker,
            IOrderRepository orderRepository)
        {
            _currentTenant = currentTenant;
            _eShopPaymentChecker = eShopPaymentChecker;
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
                
                if (!await _eShopPaymentChecker.IsValidPaymentAsync(order, eventData.Entity, item))
                {
                    // Todo: should cancel the payment?
                    throw new EShopPaymentInvalidException(eventData.Entity.Id, orderId);
                }

                order.SetPaymentId(eventData.Entity.Id);

                await _orderRepository.UpdateAsync(order, true);
            }
        }
    }
}