using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.ProductInventories.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.ProductStores;
using EasyAbp.EShop.Stores.Authorization;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Validation;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public class ProductInventoryAppService : ApplicationService, IProductInventoryAppService
    {
        private readonly IProductInventoryRepository _repository;
        private readonly IProductStoreRepository _productStoreRepository;
        private readonly DefaultProductInventoryProvider _productInventoryProvider;

        public ProductInventoryAppService(
            IProductInventoryRepository repository,
            IProductStoreRepository productStoreRepository,
            DefaultProductInventoryProvider productInventoryProvider)
        {
            _repository = repository;
            _productStoreRepository = productStoreRepository;
            _productInventoryProvider = productInventoryProvider;
        }

        [Authorize(ProductsPermissions.ProductInventory.Default)]
        public virtual async Task<ProductInventoryDto> GetAsync(Guid productId, Guid productSkuId)
        {
            var productInventory = await _repository.FindAsync(x => x.ProductSkuId == productSkuId);

            if (productInventory == null)
            {
                productInventory = new ProductInventory(GuidGenerator.Create(), productId, productSkuId, 0, 0);

                await _repository.InsertAsync(productInventory, true);
            }

            return ObjectMapper.Map<ProductInventory, ProductInventoryDto>(productInventory);
        }

        public virtual async Task<ProductInventoryDto> UpdateAsync(UpdateProductInventoryDto input)
        {
            await AuthorizationService.CheckMultiStorePolicyAsync(input.StoreId,
                ProductsPermissions.ProductInventory.Update, ProductsPermissions.ProductInventory.CrossStore);

            if (input.StoreId.HasValue)
            {
                await CheckStoreIsProductOwnerAsync(input.ProductId, input.StoreId.Value);
            }
            
            var productInventory = await _repository.FindAsync(x => x.ProductSkuId == input.ProductSkuId);

            if (productInventory == null)
            {
                productInventory =
                    new ProductInventory(GuidGenerator.Create(), input.ProductId, input.ProductSkuId, 0, 0);

                await _repository.InsertAsync(productInventory, true);
            }

            await ChangeInventoryAsync(productInventory, input.ChangedInventory);

            return ObjectMapper.Map<ProductInventory, ProductInventoryDto>(productInventory);
        }
        
        protected virtual async Task CheckStoreIsProductOwnerAsync(Guid productId, Guid storeId)
        {
            var productStore = await _productStoreRepository.GetAsync(productId, storeId);

            if (!productStore.IsOwner)
            {
                throw new StoreIsNotProductOwnerException(productId, storeId);
            }
        }

        protected virtual async Task ChangeInventoryAsync(ProductInventory productInventory, int changedInventory)
        {
            if (changedInventory >= 0)
            {
                if (!await _productInventoryProvider.TryIncreaseInventoryAsync(productInventory, changedInventory,
                    false))
                {
                    throw new InventoryChangeFailedException(productInventory.ProductId, productInventory.ProductSkuId,
                        productInventory.Inventory, changedInventory);
                }
            }
            else
            {
                if (!await _productInventoryProvider.TryReduceInventoryAsync(productInventory, -changedInventory, false)
                )
                {
                    throw new InventoryChangeFailedException(productInventory.ProductId, productInventory.ProductSkuId,
                        productInventory.Inventory, changedInventory);
                }
            }
        }
    }
}