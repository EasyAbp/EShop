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
            var uow = _unitOfWorkManager.Current;
            using (_currentTenant.Change(eventData.Entity.TenantId))
            {
                foreach (var orderLine in eventData.Entity.OrderLines)
                {
                    var product = await _productRepository.FindAsync(orderLine.ProductId);

                    var productSku = product?.ProductSkus.FirstOrDefault(sku => sku.Id == orderLine.ProductSkuId);

                    if (productSku == null)
                    {
                        await uow.RollbackAsync();
                        await _distributedEventBus.PublishAsync(new ProductInventoryReductionAfterOrderPlacedResultEto
                            {OrderId = eventData.Entity.Id, IsSuccess = false});
                        return;
                    }

                    if (product.InventoryStrategy != InventoryStrategy.ReduceAfterPlacing)
                    {
                        await uow.RollbackAsync();
                        await _distributedEventBus.PublishAsync(new ProductInventoryReductionAfterOrderPlacedResultEto
                            {OrderId = eventData.Entity.Id, IsSuccess = false});
                        return;
                    }

                    if (!await _productManager.TryReduceInventoryAsync(product, productSku, eventData.Entity.StoreId,
                        orderLine.Quantity))
                    {
                        await uow.RollbackAsync();
                        await _distributedEventBus.PublishAsync(new ProductInventoryReductionAfterOrderPlacedResultEto
                            {OrderId = eventData.Entity.Id, IsSuccess = false});
                        return;
                    }
                }
            
                await _distributedEventBus.PublishAsync(new ProductInventoryReductionAfterOrderPlacedResultEto
                    {OrderId = eventData.Entity.Id, IsSuccess = true});
            }
        }
    }
}