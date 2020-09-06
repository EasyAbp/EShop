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
        
        public virtual async Task<InventoryDataModel> GetInventoryDataAsync(Product product, ProductSku productSku, Guid storeId)
        {
            return await _productInventoryRepository.GetInventoryDataAsync(productSku.Id);
        }

        public virtual async Task<Dictionary<Guid, InventoryDataModel>> GetInventoryDataDictionaryAsync(Product product, Guid storeId)
        {
            var dict = await _productInventoryRepository.GetInventoryDataDictionaryAsync(product.ProductSkus
                .Select(sku => sku.Id).ToList());

            foreach (var sku in product.ProductSkus)
            {
                dict.GetOrAdd(sku.Id, () => new InventoryDataModel());
            }

            return dict;
        }

        public virtual async Task<bool> TryIncreaseInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity, bool decreaseSold)
        {
            var productInventory = await _productInventoryRepository.GetAsync(x => x.ProductSkuId == productSku.Id);
            
            return await TryIncreaseInventoryAsync(productInventory, quantity, decreaseSold);
        }

        public virtual async Task<bool> TryReduceInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity, bool increaseSold)
        {
            var productInventory = await _productInventoryRepository.GetAsync(x => x.ProductSkuId == productSku.Id);
            
            return await TryReduceInventoryAsync(productInventory, quantity, increaseSold);
        }
        
        public virtual async Task<bool> TryIncreaseInventoryAsync(ProductInventory productInventory, int quantity, bool decreaseSold)
        {
            if (quantity < 0)
            {
                return false;
            }
            
            var originalInventory = productInventory.Inventory;

            if (!productInventory.TryIncreaseInventory(quantity, decreaseSold))
            {
                return false;
            }

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            
            await _productInventoryRepository.UpdateAsync(productInventory, true);
            
            PublishInventoryChangedEventOnUowCompleted(uow, productInventory.ProductId, productInventory.ProductSkuId,
                originalInventory, productInventory.Inventory, productInventory.Sold);

            await uow.CompleteAsync();
            
            return true;
        }

        public virtual async Task<bool> TryReduceInventoryAsync(ProductInventory productInventory, int quantity, bool increaseSold)
        {
            if (quantity < 0)
            {
                return false;
            }

            var originalInventory = productInventory.Inventory;

            if (!productInventory.TryReduceInventory(quantity, increaseSold))
            {
                return false;
            }

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            await _productInventoryRepository.UpdateAsync(productInventory, true);

            PublishInventoryChangedEventOnUowCompleted(uow, productInventory.ProductId, productInventory.ProductSkuId,
                originalInventory, productInventory.Inventory, productInventory.Sold);

            await uow.CompleteAsync();

            return true;
        }

        protected virtual void PublishInventoryChangedEventOnUowCompleted(IUnitOfWork uow, Guid productId,
            Guid productSkuId, int originalInventory, int newInventory, long sold)
        {
            uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new ProductInventoryChangedEto
            {
                ProductId = productId,
                ProductSkuId = productSkuId,
                OriginalInventory = originalInventory,
                NewInventory = newInventory,
                Sold = sold
            }));
        }
    }
}