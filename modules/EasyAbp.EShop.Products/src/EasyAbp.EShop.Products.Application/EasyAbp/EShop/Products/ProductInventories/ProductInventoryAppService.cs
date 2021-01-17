using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.ProductInventories.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Stores.Authorization;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public class ProductInventoryAppService : ApplicationService, IProductInventoryAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductInventoryRepository _repository;
        private readonly DefaultProductInventoryProvider _productInventoryProvider;

        public ProductInventoryAppService(
            IProductRepository productRepository,
            IProductInventoryRepository repository,
            DefaultProductInventoryProvider productInventoryProvider)
        {
            _productRepository = productRepository;
            _repository = repository;
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
            var product = await _productRepository.GetAsync(input.ProductId);

            await AuthorizationService.CheckMultiStorePolicyAsync(product.StoreId,
                ProductsPermissions.ProductInventory.Update, ProductsPermissions.ProductInventory.CrossStore);
            
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