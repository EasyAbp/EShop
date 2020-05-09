using System;
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
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityCreatedEto<OrderEto> eventData)
        {
            using (_currentTenant.Change(eventData.Entity.TenantId))
            {
                var models = new List<ReduceInventoryModel>();
                
                foreach (var orderLine in eventData.Entity.OrderLines)
                {
                    var product = await _productRepository.FindAsync(orderLine.ProductId);

                    var productSku = product?.ProductSkus.FirstOrDefault(sku => sku.Id == orderLine.ProductSkuId);

                    if (productSku == null || product.InventoryStrategy != InventoryStrategy.ReduceAfterPlacing)
                    {
                        continue;
                    }

                    if (!await _productManager.IsInventorySufficientAsync(product, productSku, eventData.Entity.StoreId,
                        orderLine.Quantity))
                    {
                        await _distributedEventBus.PublishAsync(new ProductInventoryReductionAfterOrderPlacedResultEto
                            {OrderId = eventData.Entity.Id, IsSuccess = false});
                        return;
                    }

                    models.Add(new ReduceInventoryModel
                    {
                        Product = product,
                        ProductSku = productSku,
                        StoreId = eventData.Entity.StoreId,
                        Quantity = orderLine.Quantity
                    });
                }

                foreach (var model in models)
                {
                    await _productManager.TryReduceInventoryAsync(model.Product, model.ProductSku, model.StoreId,
                        model.Quantity);
                }
            
                await _distributedEventBus.PublishAsync(new ProductInventoryReductionAfterOrderPlacedResultEto
                    {OrderId = eventData.Entity.Id, IsSuccess = true});
            }
        }
    }

    internal class ReduceInventoryModel
    {
        public Product Product { get; set; }
        
        public ProductSku ProductSku { get; set; }
        
        public Guid StoreId { get; set; }
        
        public int Quantity { get; set; }
    }
}