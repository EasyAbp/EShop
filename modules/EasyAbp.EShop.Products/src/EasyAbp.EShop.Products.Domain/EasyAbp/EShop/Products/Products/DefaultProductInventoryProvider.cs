using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductInventories;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.Products
{
    public class DefaultProductInventoryProvider : IProductInventoryProvider, ITransientDependency
    {
        public string ProviderName { get; } = "Default";

        // Todo: should use IProductInventoryStore.
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IProductInventoryRepository _productInventoryRepository;

        public DefaultProductInventoryProvider(
            IUnitOfWorkManager unitOfWorkManager,
            IDistributedEventBus distributedEventBus,
            IProductInventoryRepository productInventoryRepository)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _distributedEventBus = distributedEventBus;
            _productInventoryRepository = productInventoryRepository;
        }
        
        public virtual async Task<int> GetInventoryAsync(Product product, ProductSku productSku, Guid storeId)
        {
            return await _productInventoryRepository.GetInventoryAsync(productSku.Id);
        }

        public virtual async Task<Dictionary<Guid, int>> GetInventoryDictionaryAsync(Product product, Guid storeId)
        {
            var dict = await _productInventoryRepository.GetInventoryDictionaryAsync(product.ProductSkus
                .Select(sku => sku.Id).ToList());

            foreach (var sku in product.ProductSkus)
            {
                dict.GetOrAdd(sku.Id, () => 0);
            }

            return dict;
        }

        public virtual async Task<bool> TryIncreaseInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity)
        {
            var productInventory = await _productInventoryRepository.GetAsync(x => x.ProductSkuId == productSku.Id);
            
            return await TryIncreaseInventoryAsync(productInventory, quantity);
        }

        public virtual async Task<bool> TryReduceInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity)
        {
            var productInventory = await _productInventoryRepository.GetAsync(x => x.ProductSkuId == productSku.Id);
            
            return await TryReduceInventoryAsync(productInventory, quantity);
        }
        
        public virtual async Task<bool> TryIncreaseInventoryAsync(ProductInventory productInventory, int quantity)
        {
            if (quantity < 0)
            {
                return false;
            }
            
            var originalInventory = productInventory.Inventory;

            if (!productInventory.TryIncreaseInventory(quantity))
            {
                return false;
            }

            await _productInventoryRepository.UpdateAsync(productInventory, true);
            
            PublishInventoryChangedEventOnUowCompleted(productInventory.ProductId, productInventory.ProductSkuId,
                originalInventory, productInventory.Inventory);

            return true;
        }

        public virtual async Task<bool> TryReduceInventoryAsync(ProductInventory productInventory, int quantity)
        {
            if (quantity < 0)
            {
                return false;
            }

            var originalInventory = productInventory.Inventory;

            if (!productInventory.TryReduceInventory(quantity))
            {
                return false;
            }

            await _productInventoryRepository.UpdateAsync(productInventory, true);

            PublishInventoryChangedEventOnUowCompleted(productInventory.ProductId, productInventory.ProductSkuId,
                originalInventory, productInventory.Inventory);

            return true;
        }

        protected virtual void PublishInventoryChangedEventOnUowCompleted(Guid productId, Guid productSkuId,
            int originalInventory, int newInventory)
        {
            _unitOfWorkManager.Current.OnCompleted(async () => await _distributedEventBus.PublishAsync(
                new ProductInventoryChangedEto
                {
                    ProductId = productId,
                    ProductSkuId = productSkuId,
                    OriginalInventory = originalInventory,
                    NewInventory = newInventory
                }
            ));
        }
    }
}