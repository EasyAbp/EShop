using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Products.Products
{
    public interface IOrderCreatedEventHandler : IDistributedEventHandler<EntityCreatedEto<OrderEto>>
    {
        
    }
}