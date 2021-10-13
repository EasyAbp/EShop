using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    [UnitOfWork]
    public class CancelPaymentEventHandler : IDistributedEventHandler<CancelPaymentEto>, ITransientDependency
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDistributedEventBus _distributedEventBus;

        public CancelPaymentEventHandler(
            IOrderRepository orderRepository,
            IDistributedEventBus distributedEventBus)
        {
            _orderRepository = orderRepository;
            _distributedEventBus = distributedEventBus;
        }
        
        public async Task HandleEventAsync(CancelPaymentEto eventData)
        {
            await _distributedEventBus.PublishAsync(new EShopPaymentCanceledEto(new EShopPaymentEto
            {
                TenantId = eventData.TenantId,
                Id = eventData.PaymentId,
                PaymentItems = new List<EShopPaymentItemEto>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        ItemType = PaymentsConsts.PaymentItemType,
                        ItemKey = (await _orderRepository.FirstAsync()).Id.ToString()
                    }
                }
            }));
        }
    }
}