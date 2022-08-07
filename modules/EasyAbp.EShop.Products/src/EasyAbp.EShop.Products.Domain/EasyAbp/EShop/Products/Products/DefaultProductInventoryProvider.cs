using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductInventories;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.Products
{
    public class DefaultProductInventoryProvider : IProductInventoryProvider, ITransientDependency
    {
        /// <summary>
        /// The <see cref="GetLockKeyAsync(InventoryQueryModel)"/> lock key format.
        /// <para>{0}: Tenant ID</para>
        /// <para>{1}: Product ID</para>
        /// <para>{2}: ProductSku ID</para>
        /// </summary>
        public const string DefaultProductInventoryLockKeyFormat = "eshop-product-inventory-{0}-{1}-{2}";

        public static string DefaultProductInventoryProviderName { get; set; } = "Default";
        public static string DefaultProductInventoryProviderDisplayName { get; set; } = "Default";
        public static string DefaultProductInventoryProviderDescription { get; set; } = "Default";

        public string InventoryProviderName { get; } = DefaultProductInventoryProviderName;

        // Todo: should use IProductInventoryStore.
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IProductInventoryRepository _productInventoryRepository;
        private readonly IAbpDistributedLock _distributedLock;
        private readonly ILogger<DefaultProductInventoryProvider> _logger;

        public DefaultProductInventoryProvider(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            IDistributedEventBus distributedEventBus,
            IProductInventoryRepository productInventoryRepository,
            IAbpDistributedLock distributedLock,
            ILogger<DefaultProductInventoryProvider> logger)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _distributedEventBus = distributedEventBus;
            _productInventoryRepository = productInventoryRepository;
            _distributedLock = distributedLock;
            _logger = logger;
        }

        [UnitOfWork]
        public virtual async Task<InventoryDataModel> GetInventoryDataAsync(InventoryQueryModel model)
        {
            return await _productInventoryRepository.GetInventoryDataAsync(model.ProductSkuId);
        }

        [UnitOfWork]
        public virtual async Task<Dictionary<Guid, InventoryDataModel>> GetSkuIdInventoryDataMappingAsync(
            IList<InventoryQueryModel> models)
        {
            var dict = await _productInventoryRepository.GetSkuIdInventoryDataMappingAsync(
                models.Select(x => x.ProductSkuId).ToList());

            foreach (var model in models)
            {
                dict.GetOrAdd(model.ProductSkuId, () => new InventoryDataModel());
            }

            return dict;
        }

        [UnitOfWork(true)]
        public virtual async Task<bool> TryIncreaseInventoryAsync(InventoryQueryModel model, int quantity,
            bool decreaseSold, bool isFlashSale = false)
        {
            await using var handle = await _distributedLock.TryAcquireAsync(await GetLockKeyAsync(model), TimeSpan.FromSeconds(30));

            if (handle == null)
            {
                _logger.LogWarning("TryIncreaseInventory failed to acquire lock for product inventory: {TenantId},{ProductId},{ProductSkuId}",
                    model.TenantId, model.ProductId, model.ProductSkuId);
                return false;
            }

            var productInventory = await GetOrCreateProductInventoryAsync(model.ProductId, model.ProductSkuId);

            return await TryIncreaseInventoryAsync(model, productInventory, quantity, decreaseSold);
        }

        [UnitOfWork(true)]
        public virtual async Task<bool> TryReduceInventoryAsync(InventoryQueryModel model, int quantity,
            bool increaseSold, bool isFlashSale = false)
        {
            await using var handle = await _distributedLock.TryAcquireAsync(await GetLockKeyAsync(model), TimeSpan.FromSeconds(30));

            if (handle == null)
            {
                _logger.LogWarning("TryReduceInventory failed to acquire lock for product inventory: {TenantId},{ProductId},{ProductSkuId}",
                    model.TenantId, model.ProductId, model.ProductSkuId);
                return false;
            }

            var productInventory = await GetOrCreateProductInventoryAsync(model.ProductId, model.ProductSkuId);

            return await TryReduceInventoryAsync(model, productInventory, quantity, increaseSold);
        }

        [UnitOfWork]
        protected virtual async Task<ProductInventory> GetOrCreateProductInventoryAsync(Guid productId,
            Guid productSkuId)
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
        protected virtual async Task<bool> TryIncreaseInventoryAsync(InventoryQueryModel model,
            ProductInventory productInventory, int quantity, bool decreaseSold)
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

            await PublishInventoryChangedEventAsync(model.TenantId, model.StoreId,
                productInventory.ProductId, productInventory.ProductSkuId, originalInventory,
                productInventory.Inventory, productInventory.Sold);

            return true;
        }

        [UnitOfWork(true)]
        protected virtual async Task<bool> TryReduceInventoryAsync(InventoryQueryModel model,
            ProductInventory productInventory, int quantity, bool increaseSold)
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

            await PublishInventoryChangedEventAsync(model.TenantId, model.StoreId,
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

        protected virtual Task<string> GetLockKeyAsync(InventoryQueryModel model)
        {
            return Task.FromResult(string.Format(DefaultProductInventoryLockKeyFormat, model.TenantId, model.ProductId, model.ProductSkuId));
        }
    }
}