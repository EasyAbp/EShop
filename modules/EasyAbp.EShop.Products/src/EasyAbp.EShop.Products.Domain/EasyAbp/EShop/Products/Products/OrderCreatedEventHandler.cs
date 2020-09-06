using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.Products
{
    public class OrderCreatedEventHandler : IOrderCreatedEventHandler, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IProductRepository _productRepository;
        private readonly IProductManager _productManager;

        public OrderCreatedEventHandler(
            ICurrentTenant currentTenant,
            IUnitOfWorkManager unitOfWorkManager,
            IDistributedEventBus distributedEventBus,
            IProductRepository productRepository,
            IProductManager productManager)
        {
            _currentTenant = currentTenant;
            _unitOfWorkManager = unitOfWorkManager;
            _distributedEventBus = distributedEventBus;
            _productRepository = productRepository;
            _productManager = productManager;
        }
        
        public virtual async Task HandleEventAsync(EntityCreatedEto<OrderEto> eventData)
        {
            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            using var changeTenant = _currentTenant.Change(eventData.Entity.TenantId);

            var models = new List<ConsumeInventoryModel>();
                
            foreach (var orderLine in eventData.Entity.OrderLines)
            {
                // Todo: Should use ProductHistory.
                var product = await _productRepository.FindAsync(orderLine.ProductId);

                var productSku = product?.ProductSkus.FirstOrDefault(sku => sku.Id == orderLine.ProductSkuId);

                if (productSku == null)
                {
                    await PublishResultEventAsync(eventData, false);
                    
                    return;
                }
                    
                if (product.InventoryStrategy != InventoryStrategy.ReduceAfterPlacing)
                {
                    continue;
                }

                if (!await _productManager.IsInventorySufficientAsync(product, productSku, eventData.Entity.StoreId,
                    orderLine.Quantity))
                {
                    await PublishResultEventAsync(eventData, false);
                    
                    return;
                }

                models.Add(new ConsumeInventoryModel
                {
                    Product = product,
                    ProductSku = productSku,
                    StoreId = eventData.Entity.StoreId,
                    Quantity = orderLine.Quantity
                });
            }

            foreach (var model in models)
            {
                if (await _productManager.TryReduceInventoryAsync(model.Product, model.ProductSku, model.StoreId,
                    model.Quantity, true))
                {
                    continue;
                }

                await uow.RollbackAsync();
                
                await PublishResultEventAsync(eventData, false);
                
                return;
            }

            await uow.CompleteAsync();
            
            await PublishResultEventAsync(eventData, true);
        }
        
        protected virtual async Task PublishResultEventAsync(EntityCreatedEto<OrderEto> orderCreatedEto, bool isSuccess)
        {
            await _distributedEventBus.PublishAsync(new ProductInventoryReductionAfterOrderPlacedResultEto
            {
                TenantId = orderCreatedEto.Entity.TenantId,
                OrderId = orderCreatedEto.Entity.Id,
                IsSuccess = isSuccess
            });
        }
    }
}