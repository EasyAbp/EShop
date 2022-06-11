using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Refunds;
using EasyAbp.EShop.Products.Products;
using EasyAbp.PaymentService.Refunds;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class ProductInventoryReductionEventHandler : IProductInventoryReductionEventHandler, ITransientDependency
    {
        private readonly IClock _clock;
        private readonly ICurrentTenant _currentTenant;
        private readonly IOrderManager _orderManager;
        private readonly IOrderRepository _orderRepository;
        private readonly IDistributedEventBus _distributedEventBus;

        public ProductInventoryReductionEventHandler(
            IClock clock,
            ICurrentTenant currentTenant,
            IOrderManager orderManager,
            IOrderRepository orderRepository,
            IDistributedEventBus distributedEventBus)
        {
            _clock = clock;
            _currentTenant = currentTenant;
            _orderManager = orderManager;
            _orderRepository = orderRepository;
            _distributedEventBus = distributedEventBus;
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
                    await _orderManager.CancelAsync(order, OrdersConsts.InventoryReductionFailedAutoCancellationReason);

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
                    var refundOrderEto = CreateRefundOrderEto(order);

                    await _orderManager.CancelAsync(order, OrdersConsts.InventoryReductionFailedAutoCancellationReason);

                    await RefundOrderAsync(refundOrderEto);

                    return;
                }

                order.SetReducedInventoryAfterPaymentTime(_clock.Now);

                await _orderRepository.UpdateAsync(order, true);
            }
        }

        [UnitOfWork(true)]
        protected virtual async Task RefundOrderAsync(RefundOrderEto refundOrderEto)
        {
            await _distributedEventBus.PublishAsync(refundOrderEto);
        }

        protected virtual RefundOrderEto CreateRefundOrderEto(Order order)
        {
            if (!order.PaymentId.HasValue)
            {
                throw new OrderIsInWrongStageException(order.Id);
            }

            var eto = new RefundOrderEto(
                order.TenantId,
                order.Id,
                order.StoreId,
                order.PaymentId.Value,
                OrdersConsts.InventoryReductionFailedAutoCancellationReason,
                OrdersConsts.InventoryReductionFailedAutoCancellationReason,
                OrdersConsts.InventoryReductionFailedAutoCancellationReason);
            
            eto.OrderLines.AddRange(order.OrderLines.Select(x => new OrderLineRefundInfoModel
            {
                OrderLineId = x.Id,
                Quantity = x.Quantity - x.RefundedQuantity,
                TotalAmount = x.ActualTotalPrice - x.RefundAmount
            }));
            
            eto.OrderExtraFees.AddRange(order.OrderExtraFees.Select(x => new OrderExtraFeeRefundInfoModel
            {
                Name = x.Name,
                Key = x.Key,
                TotalAmount = x.Fee - x.RefundAmount
            }));

            return eto;
        }
    }
}