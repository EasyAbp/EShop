using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.Products
{
    // see: https://github.com/EasyAbp/EShop/issues/139
    public class InventoryRollbackOrderCanceledEventHandler : IDistributedEventHandler<OrderCanceledEto>, ITransientDependency
    {
        private readonly IProductManager _productManager;
        private readonly IProductRepository _productRepository;

        public InventoryRollbackOrderCanceledEventHandler(
            IProductManager productManager,
            IProductRepository productRepository)
        {
            _productManager = productManager;
            _productRepository = productRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(OrderCanceledEto eventData)
        {
            if (eventData.Order.PaidTime.HasValue)
            {
                return;
            }
            
            foreach (var orderLine in eventData.Order.OrderLines)
            {
                var product = await _productRepository.GetAsync(orderLine.ProductId);

                if (product.InventoryStrategy is InventoryStrategy.NoNeed or InventoryStrategy.ReduceAfterPayment)
                {
                    continue;
                }

                var productSku = product.ProductSkus.Single(x => x.Id == orderLine.ProductSkuId);

                await _productManager.TryIncreaseInventoryAsync(product, productSku, orderLine.Quantity, true);
            }
        }
    }
}