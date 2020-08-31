using EasyAbp.EShop.Payments.Payments;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IPaymentCompletedEventHandler : IDistributedEventHandler<EShopPaymentCompletedEto>
    {
        
    }
}