using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.ProductInventories.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.ProductStores;
using EasyAbp.EShop.Stores.Permissions;
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

        [Authorize(ProductsPermissions.ProductInventory.Update)]
        public virtual async Task<ProductInventoryDto> UpdateAsync(UpdateProductInventoryDto input)
        {
            if (!await AuthorizationService.IsGrantedAsync(ProductsPermissions.ProductInventory.CrossStore))
            {
                if (!input.StoreId.HasValue)
                {
                    throw new AbpValidationException("StoreId should not be null.");
                }

                await AuthorizationService.CheckMultiStorePolicyAsync(input.StoreId.Value,
                    ProductsPermissions.ProductInventory.Update);

                await _productStoreRepository.GetAsync(input.ProductId, input.StoreId.Value);
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