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
        private readonly IMoneyDistributor _moneyDistributor;

        public PaymentCreatedEventHandler(
            ICurrentTenant currentTenant,
            IOrderPaymentChecker orderPaymentChecker,
            IOrderRepository orderRepository,
            IMoneyDistributor moneyDistributor)
        {
            _currentTenant = currentTenant;
            _orderPaymentChecker = orderPaymentChecker;
            _orderRepository = orderRepository;
            _moneyDistributor = moneyDistributor;
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityCreatedEto<EShopPaymentEto> eventData)
        {
            using var currentTenant = _currentTenant.Change(eventData.Entity.TenantId);

            foreach (var item in eventData.Entity.PaymentItems.Where(item =>
                         item.ItemType == PaymentsConsts.PaymentItemType))
            {
                await HandleEventItemAsync(eventData, item);
            }
        }

        protected virtual async Task HandleEventItemAsync(EntityCreatedEto<EShopPaymentEto> eventData, EShopPaymentItemEto item)
        {
            var orderId = Guid.Parse(item.ItemKey);

            var order = await _orderRepository.GetAsync(orderId);

            if (order.PaymentId.HasValue || order.OrderStatus != OrderStatus.Pending)
            {
                OrderIsInWrongStageException(order);
            }

            if (!await _orderPaymentChecker.IsValidPaymentAsync(order, eventData.Entity, item))
            {
                InvalidPaymentException(eventData.Entity, order);
            }

            await order.StartPaymentAsync(eventData.Entity.Id, item.ActualPaymentAmount, _moneyDistributor);

            await _orderRepository.UpdateAsync(order, true);
        }

        protected virtual void InvalidPaymentException(EShopPaymentEto entity, Order order)
        {
            throw new InvalidPaymentException(entity.Id, order.Id);
        }

        protected virtual void OrderIsInWrongStageException(Order order)
        {
            throw new OrderIsInWrongStageException(order.Id);
        }
    }
}