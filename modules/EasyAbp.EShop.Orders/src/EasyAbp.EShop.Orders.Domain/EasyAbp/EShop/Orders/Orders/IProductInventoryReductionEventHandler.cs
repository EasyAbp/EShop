using EasyAbp.EShop.Products.Products;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IProductInventoryReductionEventHandler :
        IDistributedEventHandler<ProductInventoryReductionAfterOrderPlacedResultEto>,
        IDistributedEventHandler<ProductInventoryReductionAfterOrderPaidResultEto>
    {
        
    }
}