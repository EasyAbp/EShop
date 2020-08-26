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
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class RefundSynchronizer :
        IDistributedEventHandler<EntityUpdatedEto<EShopRefundEto>>,
        IDistributedEventHandler<EntityDeletedEto<EShopRefundEto>>,
        IRefundSynchronizer,
        ITransientDependency
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IRefundRepository _refundRepository;

        public RefundSynchronizer(
            IObjectMapper objectMapper,
            IGuidGenerator guidGenerator,
            IJsonSerializer jsonSerializer,
            IRefundRepository refundRepository)
        {
            _objectMapper = objectMapper;
            _guidGenerator = guidGenerator;
            _jsonSerializer = jsonSerializer;
            _refundRepository = refundRepository;
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityUpdatedEto<EShopRefundEto> eventData)
        {
            var refund = await _refundRepository.FindAsync(eventData.Entity.Id);
            
            if (refund == null)
            {
                refund = _objectMapper.Map<EShopRefundEto, Refund>(eventData.Entity);

                refund.SetRefundItems(
                    _objectMapper.Map<List<EShopRefundItemEto>, List<RefundItem>>(eventData.Entity.RefundItems));

                await _refundRepository.InsertAsync(refund, true);
            }
            else
            {
                _objectMapper.Map(eventData.Entity, refund);

                foreach (var etoItem in eventData.Entity.RefundItems)
                {
                    var item = refund.RefundItems.FirstOrDefault(i => i.Id == etoItem.Id);

                    if (item == null)
                    {
                        if (!Guid.TryParse(etoItem.GetProperty<string>("StoreId"), out var storeId))
                        {
                            throw new StoreIdNotFoundException();
                        }
                        
                        if (!Guid.TryParse(etoItem.GetProperty<string>("OrderId"), out var orderId))
                        {
                            throw new OrderIdNotFoundException();
                        }
                        
                        item = _objectMapper.Map<EShopRefundItemEto, RefundItem>(etoItem);
                        
                        item.SetStoreId(storeId);
                        item.SetOrderId(orderId);

                        refund.RefundItems.Add(item);
                    }
                    else
                    {
                        _objectMapper.Map(etoItem, item);
                    }
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
        }

        public virtual async Task HandleEventAsync(EntityDeletedEto<EShopRefundEto> eventData)
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
