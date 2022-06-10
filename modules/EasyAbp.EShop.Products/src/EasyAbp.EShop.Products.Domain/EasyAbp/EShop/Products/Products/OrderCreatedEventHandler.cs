using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.Products
{
    public class OrderCreatedEventHandler : IDistributedEventHandler<EntityCreatedEto<OrderEto>>, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly ILogger<OrderCreatedEventHandler> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IProductManager _productManager;

        public OrderCreatedEventHandler(
            ICurrentTenant currentTenant,
            IUnitOfWorkManager unitOfWorkManager,
            IDistributedEventBus distributedEventBus,
            ILogger<OrderCreatedEventHandler> logger,
            IProductRepository productRepository,
            IProductManager productManager)
        {
            _currentTenant = currentTenant;
            _unitOfWorkManager = unitOfWorkManager;
            _distributedEventBus = distributedEventBus;
            _logger = logger;
            _productRepository = productRepository;
            _productManager = productManager;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityCreatedEto<OrderEto> eventData)
        {
            using var changeTenant = _currentTenant.Change(eventData.Entity.TenantId);

            var models = new List<ConsumeInventoryModel>();
                
            foreach (var orderLine in eventData.Entity.OrderLines)
            {
                // Todo: Should use ProductHistory.
                var product = await _productRepository.FindAsync(orderLine.ProductId);

                var productSku = product?.ProductSkus.FirstOrDefault(sku => sku.Id == orderLine.ProductSkuId);

                if (productSku == null)
                {
                    await PublishInventoryReductionResultEventAsync(eventData, false);
                    
                    return;
                }
                    
                if (product.InventoryStrategy != InventoryStrategy.ReduceAfterPlacing)
                {
                    continue;
                }

                if (!await _productManager.IsInventorySufficientAsync(product, productSku, orderLine.Quantity))
                {
                    await PublishInventoryReductionResultEventAsync(eventData, false);
                    
                    return;
                }

                models.Add(new ConsumeInventoryModel(
                    product, productSku, eventData.Entity.StoreId, orderLine.Quantity));

            }

            foreach (var model in models)
            {
                if (await _productManager.TryReduceInventoryAsync(
                        model.Product, model.ProductSku, model.Quantity, true))
                {
                    continue;
                }

                await TryRollbackInventoriesAsync(models);

                await _unitOfWorkManager.Current.RollbackAsync();

                await PublishInventoryReductionResultEventAsync(eventData, false, true);

                return;
            }

            await PublishInventoryReductionResultEventAsync(eventData, true);
        }
        
        protected virtual async Task<bool> TryRollbackInventoriesAsync(IEnumerable<ConsumeInventoryModel> models)
        {
            var result = true;

            foreach (var model in models.Where(x => x.Consumed))
            {
                if (await _productManager.TryIncreaseInventoryAsync(
                        model.Product, model.ProductSku, model.Quantity, true))
                {
                    continue;
                }

                result = false;
                _logger.LogWarning(
                    "OrderCreatedEventHandler: inventory rollback failed! productId = {productId}, productSkuId = {productSkuId}, quantity = {quantity}, reduceSold = {reduceSold}",
                    model.Product.Id, model.ProductSku.Id, model.Quantity, true);
            }

            return result;
        }
        
        protected virtual async Task PublishInventoryReductionResultEventAsync(EntityCreatedEto<OrderEto> orderCreatedEto, bool isSuccess, bool publishNow = false)
        {
            await _distributedEventBus.PublishAsync(new ProductInventoryReductionAfterOrderPlacedResultEto
            {
                TenantId = orderCreatedEto.Entity.TenantId,
                OrderId = orderCreatedEto.Entity.Id,
                IsSuccess = isSuccess
            }, !publishNow, !publishNow);
        }
    }
}