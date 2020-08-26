using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        IDistributedEventHandler<EntityUpdatedEto<RefundEto>>,
        IDistributedEventHandler<EntityDeletedEto<RefundEto>>,
        IRefundSynchronizer,
        ITransientDependency
    {
        private readonly IObjectMapper _objectMapper;
        private readonly ICurrentTenant _currentTenant;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IRefundRepository _refundRepository;

        public RefundSynchronizer(
            IObjectMapper objectMapper,
            ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator,
            IJsonSerializer jsonSerializer,
            IUnitOfWorkManager unitOfWorkManager,
            IDistributedEventBus distributedEventBus,
            IRefundRepository refundRepository)
        {
            _objectMapper = objectMapper;
            _currentTenant = currentTenant;
            _guidGenerator = guidGenerator;
            _jsonSerializer = jsonSerializer;
            _unitOfWorkManager = unitOfWorkManager;
            _distributedEventBus = distributedEventBus;
            _refundRepository = refundRepository;
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityUpdatedEto<RefundEto> eventData)
        {
            using var changeTenant = _currentTenant.Change(eventData.Entity.TenantId);
            
            var publishOrderRefundCompletedEvent = false;
            
            var refund = await _refundRepository.FindAsync(eventData.Entity.Id);
            
            if (refund == null)
            {
                refund = _objectMapper.Map<RefundEto, Refund>(eventData.Entity);

                refund.SetRefundItems(
                    _objectMapper.Map<List<RefundItemEto>, List<RefundItem>>(eventData.Entity.RefundItems));

                refund.RefundItems.ForEach(item =>
                {
                    FillRefundItemStoreId(item);
                    FillRefundItemOrderId(item);
                });
                
                if (refund.CompletedTime.HasValue)
                {
                    publishOrderRefundCompletedEvent = true;
                }
                
                await _refundRepository.InsertAsync(refund, true);
            }
            else
            {
                if (eventData.Entity.CompletedTime.HasValue && !refund.CompletedTime.HasValue)
                {
                    publishOrderRefundCompletedEvent = true;
                }
                
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
            }

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

            await _refundRepository.UpdateAsync(refund, true);

            var orderRefundEto = _objectMapper.Map<Refund, OrderRefundEto>(refund);

            if (publishOrderRefundCompletedEvent)
            {
                _unitOfWorkManager.Current.OnCompleted(async () =>
                    await _distributedEventBus.PublishAsync(new OrderRefundCompletedEto {Refund = orderRefundEto}));
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
        
        public virtual async Task HandleEventAsync(EntityDeletedEto<RefundEto> eventData)
        {
            var refund = await _refundRepository.FindAsync(eventData.Entity.Id);

            if (refund == null)
            {
                return;
            }
            
            await _refundRepository.DeleteAsync(refund, true);
        }
    }
}
