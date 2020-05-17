using EasyAbp.PaymentService.Payments;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderPaymentCompletedEventHandler : IDistributedEventHandler<EntityUpdatedEto<PaymentEto>>
    {
        
    }
}