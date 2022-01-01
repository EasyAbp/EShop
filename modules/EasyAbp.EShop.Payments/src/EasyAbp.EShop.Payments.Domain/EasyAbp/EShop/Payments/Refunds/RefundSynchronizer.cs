using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.PaymentService.Refunds;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.Json;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class RefundSynchronizer :
        IDistributedEventHandler<EntityCreatedEto<RefundEto>>,
        IDistributedEventHandler<EntityUpdatedEto<RefundEto>>,
        IDistributedEventHandler<EntityDeletedEto<RefundEto>>,
        IRefundSynchronizer,
        ITransientDependency
    {
        private readonly IObjectMapper _objectMapper;
        private readonly ICurrentTenant _currentTenant;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IRefundRepository _refundRepository;

        public RefundSynchronizer(
            IObjectMapper objectMapper,
            ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator,
            IJsonSerializer jsonSerializer,
            IDistributedEventBus distributedEventBus,
            IPaymentRepository paymentRepository,
            IRefundRepository refundRepository)
        {
            _objectMapper = objectMapper;
            _currentTenant = currentTenant;
            _guidGenerator = guidGenerator;
            _jsonSerializer = jsonSerializer;
            _distributedEventBus = distributedEventBus;
            _paymentRepository = paymentRepository;
            _refundRepository = refundRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityCreatedEto<RefundEto> eventData)
        {
            using var changeTenant = _currentTenant.Change(eventData.Entity.TenantId);
            
            var refund = await _refundRepository.FindAsync(eventData.Entity.Id);

            if (refund != null)
            {
                return;
            }
            
            var payment = await _paymentRepository.FindAsync(eventData.Entity.PaymentId);

            if (payment == null)
            {
                return;
            }
                
            refund = _objectMapper.Map<RefundEto, Refund>(eventData.Entity);

            refund.SetRefundItems(
                _objectMapper.Map<List<RefundItemEto>, List<RefundItem>>(eventData.Entity.RefundItems));

            refund.RefundItems.ForEach(item =>
            {
                FillRefundItemStoreId(item);
                FillRefundItemOrderId(item);
            });

            FillRefundItemOrderLines(refund);
                
            await _refundRepository.InsertAsync(refund, true);
            
            if (refund.CompletedTime.HasValue)
            {
                await _distributedEventBus.PublishAsync(new EShopRefundCompletedEto
                {
                    Refund = _objectMapper.Map<Refund, EShopRefundEto>(refund)
                });
            }
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityUpdatedEto<RefundEto> eventData)
        {
            using var changeTenant = _currentTenant.Change(eventData.Entity.TenantId);
            
            var refund = await _refundRepository.FindAsync(eventData.Entity.Id);
            
            if (refund == null)
            {
                return;
            }

            var publishRefundCompleted = eventData.Entity.CompletedTime.HasValue && !refund.CompletedTime.HasValue;
                
            _objectMapper.Map(eventData.Entity, refund);

            foreach (var etoItem in eventData.Entity.RefundItems)
            {
                var item = refund.RefundItems.FirstOrDefault(i => i.Id == etoItem.Id);

                if (item == null)
                {
                    item = _objectMapper.Map<RefundItemEto, RefundItem>(etoItem);
                        
                    refund.RefundItems.Add(item);
                }
                else
                {
                    _objectMapper.Map(etoItem, item);
                }

                FillRefundItemStoreId(item);
                FillRefundItemOrderId(item);
            }
                
            var etoRefundItemIds = eventData.Entity.RefundItems.Select(i => i.Id).ToList();

            refund.RefundItems.RemoveAll(i => !etoRefundItemIds.Contains(i.Id));

            FillRefundItemOrderLines(refund);

            await _refundRepository.UpdateAsync(refund, true);

            if (publishRefundCompleted)
            {
                await _distributedEventBus.PublishAsync(new EShopRefundCompletedEto
                {
                    Refund = _objectMapper.Map<Refund, EShopRefundEto>(refund)
                });
            }
        }

        protected virtual void FillRefundItemOrderLines(Refund refund)
        {
            foreach (var refundItem in refund.RefundItems)
            {
                var orderLineInfoModels =
                    _jsonSerializer.Deserialize<List<OrderLineRefundInfoModel>>(
                        refundItem.GetProperty<string>("OrderLines"));
                
                foreach (var orderLineInfoModel in orderLineInfoModels)
                {
                    var refundItemOrderLineEntity =
                        refundItem.RefundItemOrderLines.FirstOrDefault(x =>
                            x.OrderLineId == orderLineInfoModel.OrderLineId);

                    if (refundItemOrderLineEntity == null)
                    {
                        refundItemOrderLineEntity = new RefundItemOrderLine(_guidGenerator.Create(),
                            orderLineInfoModel.OrderLineId, orderLineInfoModel.Quantity,
                            orderLineInfoModel.TotalAmount);
                        
                        refundItem.RefundItemOrderLines.Add(refundItemOrderLineEntity);
                    }

                    var orderLineIds = orderLineInfoModels.Select(i => i.OrderLineId).ToList();

                    refundItem.RefundItemOrderLines.RemoveAll(i => !orderLineIds.Contains(i.OrderLineId));
                }
            }
        }

        protected virtual void FillRefundItemStoreId(RefundItem item)
        {
            if (!Guid.TryParse(item.GetProperty<string>("StoreId"), out var storeId))
            {
                throw new StoreIdNotFoundException();
            }
                    
            item.SetStoreId(storeId);
        }
        
        protected virtual void FillRefundItemOrderId(RefundItem item)
        {
            if (!Guid.TryParse(item.GetProperty<string>("OrderId"), out var orderId))
            {
                throw new OrderIdNotFoundException();
            }
            
            item.SetOrderId(orderId);
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityDeletedEto<RefundEto> eventData)
        {
            using var changeTenant = _currentTenant.Change(eventData.Entity.TenantId);
            
            var refund = await _refundRepository.FindAsync(eventData.Entity.Id);

            if (refund == null)
            {
                return;
            }
            
            await _refundRepository.DeleteAsync(refund, true);
        }
    }
}
