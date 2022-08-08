using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.Products
{
    // see: https://github.com/EasyAbp/EShop/issues/139
    public class InventoryRollbackOrderCanceledEventHandler : IDistributedEventHandler<OrderCanceledEto>, ITransientDependency
    {
        private readonly ILogger<InventoryRollbackOrderCanceledEventHandler> _logger;
        private readonly IProductManager _productManager;
        private readonly IProductRepository _productRepository;

        public InventoryRollbackOrderCanceledEventHandler(
            ILogger<InventoryRollbackOrderCanceledEventHandler> logger,
            IProductManager productManager,
            IProductRepository productRepository)
        {
            _logger = logger;
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

                if (!await _productManager.TryIncreaseInventoryAsync(product, productSku, orderLine.Quantity, true))
                {
                    _logger.LogWarning(
                        "InventoryRollbackOrderCanceledEventHandler: inventory rollback failed! productId = {productId}, productSkuId = {productSkuId}, quantity = {quantity}, reduceSold = {reduceSold}",
                        orderLine.ProductId, orderLine.ProductSkuId, orderLine.Quantity, true);
                }
            }
        }
    }
}