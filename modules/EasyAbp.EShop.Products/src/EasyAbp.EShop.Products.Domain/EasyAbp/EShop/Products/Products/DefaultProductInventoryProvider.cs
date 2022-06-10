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
        public static string DefaultProductInventoryProviderName { get; set; } = "Default";
        public static string DefaultProductInventoryProviderDisplayName { get; set; } = "Default";
        public static string DefaultProductInventoryProviderDescription { get; set; } = "Default";

        public string InventoryProviderName { get; } = DefaultProductInventoryProviderName;

        // Todo: should use IProductInventoryStore.
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IProductInventoryRepository _productInventoryRepository;

        public DefaultProductInventoryProvider(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            IDistributedEventBus distributedEventBus,
            IProductInventoryRepository productInventoryRepository)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _distributedEventBus = distributedEventBus;
            _productInventoryRepository = productInventoryRepository;
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
            bool decreaseSold)
        {
            var productInventory = await GetOrCreateProductInventoryAsync(model.ProductId, model.ProductSkuId);

            return await TryIncreaseInventoryAsync(model, productInventory, quantity, decreaseSold);
        }

        [UnitOfWork(true)]
        public virtual async Task<bool> TryReduceInventoryAsync(InventoryQueryModel model, int quantity,
            bool increaseSold)
        {
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
    }
}