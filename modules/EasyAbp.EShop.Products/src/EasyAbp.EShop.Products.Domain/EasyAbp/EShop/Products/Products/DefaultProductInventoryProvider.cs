using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductInventories;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.Products
{
    public class DefaultProductInventoryProvider : IProductInventoryProvider, ITransientDependency
    {
        // Todo: should use IProductInventoryStore.
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IProductInventoryRepository _productInventoryRepository;

        public DefaultProductInventoryProvider(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            IUnitOfWorkManager unitOfWorkManager,
            IDistributedEventBus distributedEventBus,
            IProductInventoryRepository productInventoryRepository)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _unitOfWorkManager = unitOfWorkManager;
            _distributedEventBus = distributedEventBus;
            _productInventoryRepository = productInventoryRepository;
        }
        
        [UnitOfWork]
        public virtual async Task<InventoryDataModel> GetInventoryDataAsync(Product product, ProductSku productSku)
        {
            return await _productInventoryRepository.GetInventoryDataAsync(productSku.Id);
        }

        [UnitOfWork]
        public virtual async Task<Dictionary<Guid, InventoryDataModel>> GetInventoryDataDictionaryAsync(Product product)
        {
            var dict = await _productInventoryRepository.GetInventoryDataDictionaryAsync(product.ProductSkus
                .Select(sku => sku.Id).ToList());

            foreach (var sku in product.ProductSkus)
            {
                dict.GetOrAdd(sku.Id, () => new InventoryDataModel());
            }

            return dict;
        }

        [UnitOfWork(true)]
        public virtual async Task<bool> TryIncreaseInventoryAsync(Product product, ProductSku productSku, int quantity, bool decreaseSold)
        {
            var productInventory = await GetOrCreateProductInventoryAsync(product.Id, productSku.Id);
            
            return await TryIncreaseInventoryAsync(product, productInventory, quantity, decreaseSold);
        }

        [UnitOfWork(true)]
        public virtual async Task<bool> TryReduceInventoryAsync(Product product, ProductSku productSku, int quantity, bool increaseSold)
        {
            var productInventory = await GetOrCreateProductInventoryAsync(product.Id, productSku.Id);
            
            return await TryReduceInventoryAsync(product, productInventory, quantity, increaseSold);
        }
        
        [UnitOfWork]
        protected virtual async Task<ProductInventory> GetOrCreateProductInventoryAsync(Guid productId, Guid productSkuId)
        {
            var productInventory =
                await _productInventoryRepository.FindAsync(x =>
                    x.ProductId == productId && x.ProductSkuId == productSkuId);
            
            if (productInventory is null)
            {
                productInventory = new ProductInventory(_guidGenerator.Create(), _currentTenant.Id, productId,
                    productSkuId, 0, 0);

                await _productInventoryRepository.InsertAsync(productInventory, true);
            }

            return productInventory;
        }
        
        [UnitOfWork(true)]
        public virtual async Task<bool> TryIncreaseInventoryAsync(Product product, ProductInventory productInventory, int quantity, bool decreaseSold)
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

            await _productInventoryRepository.UpdateAsync(productInventory, true);

            await PublishInventoryChangedEventAsync(product.TenantId, product.StoreId,
                productInventory.ProductId, productInventory.ProductSkuId, originalInventory,
                productInventory.Inventory, productInventory.Sold);
            
            return true;
        }

        [UnitOfWork(true)]
        public virtual async Task<bool> TryReduceInventoryAsync(Product product, ProductInventory productInventory, int quantity, bool increaseSold)
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

            await _productInventoryRepository.UpdateAsync(productInventory, true);

            await PublishInventoryChangedEventAsync(product.TenantId, product.StoreId,
                productInventory.ProductId, productInventory.ProductSkuId, originalInventory,
                productInventory.Inventory, productInventory.Sold);

            return true;
        }

        [UnitOfWork]
        protected virtual async Task PublishInventoryChangedEventAsync(Guid? tenantId, Guid storeId,
            Guid productId, Guid productSkuId, int originalInventory, int newInventory, long sold)
        {
            await _distributedEventBus.PublishAsync(new ProductInventoryChangedEto(
                tenantId,
                storeId,
                productId,
                productSkuId,
                originalInventory,
                newInventory,
                sold));
        }
    }
}