using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.Products
{
    public class OrderPaidEventHandler : IOrderPaidEventHandler, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IProductRepository _productRepository;
        private readonly IProductManager _productManager;

        public OrderPaidEventHandler(
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
        public virtual async Task HandleEventAsync(OrderPaidEto eventData)
        {
            using var changeTenant = _currentTenant.Change(eventData.Order.TenantId);

            var models = new List<ConsumeInventoryModel>();
                
            foreach (var orderLine in eventData.Order.OrderLines)
            {
                // Todo: Should use ProductHistory.
                var product = await _productRepository.FindAsync(orderLine.ProductId);

                var productSku = product?.ProductSkus.FirstOrDefault(sku => sku.Id == orderLine.ProductSkuId);

                if (productSku == null)
                {
                    await PublishInventoryReductionResultEventAsync(eventData, false);
                    
                    return;
                }
                    
                if (product.InventoryStrategy != InventoryStrategy.ReduceAfterPayment)
                {
                    continue;
                }

                if (!await _productManager.IsInventorySufficientAsync(product, productSku, orderLine.Quantity))
                {
                    await PublishInventoryReductionResultEventAsync(eventData, false);
                    
                    return;
                }

                models.Add(new ConsumeInventoryModel
                {
                    Product = product,
                    ProductSku = productSku,
                    StoreId = eventData.Order.StoreId,
                    Quantity = orderLine.Quantity
                });
            }

            foreach (var model in models)
            {
                if (await _productManager.TryReduceInventoryAsync(model.Product, model.ProductSku, model.Quantity, true))
                {
                    continue;
                }

                await _unitOfWorkManager.Current.RollbackAsync();
                
                await PublishInventoryReductionResultEventAsync(eventData, false, true);
                
                return;
            }

            await PublishInventoryReductionResultEventAsync(eventData, true);
        }

        protected virtual async Task PublishInventoryReductionResultEventAsync(OrderPaidEto orderPaidEto, bool isSuccess, bool publishNow = false)
        {
            await _distributedEventBus.PublishAsync(new ProductInventoryReductionAfterOrderPaidResultEto
            {
                TenantId = orderPaidEto.Order.TenantId,
                OrderId = orderPaidEto.Order.Id,
                PaymentId = orderPaidEto.PaymentId,
                PaymentItemId = orderPaidEto.PaymentItemId,
                IsSuccess = isSuccess
            }, !publishNow, !publishNow);
        }
    }
}