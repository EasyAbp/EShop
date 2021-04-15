using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Options.ProductGroups;
using EasyAbp.EShop.Products.ProductCategories;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductManager : DomainService, IProductManager
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductPriceProvider _productPriceProvider;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductInventoryProvider _productInventoryProvider;
        private readonly IProductGroupConfigurationProvider _productGroupConfigurationProvider;

        public ProductManager(
            IProductRepository productRepository,
            IProductPriceProvider productPriceProvider,
            IProductCategoryRepository productCategoryRepository,
            IProductInventoryProvider productInventoryProvider,
            IProductGroupConfigurationProvider productGroupConfigurationProvider)
        {
            _productRepository = productRepository;
            _productPriceProvider = productPriceProvider;
            _productCategoryRepository = productCategoryRepository;
            _productInventoryProvider = productInventoryProvider;
            _productGroupConfigurationProvider = productGroupConfigurationProvider;
        }

        public virtual async Task<Product> CreateAsync(Product product, IEnumerable<Guid> categoryIds = null)
        {
            product.TrimUniqueName();
            
            await CheckProductGroupNameAsync(product);
            
            await CheckProductUniqueNameAsync(product);

            await _productRepository.InsertAsync(product, autoSave: true);

            await CheckProductDetailAvailableAsync(product.Id, product.ProductDetailId);

            await UpdateProductCategoriesAsync(product.Id, categoryIds);

            return product;
        }

        protected virtual Task CheckProductGroupNameAsync(Product product)
        {
            if (_productGroupConfigurationProvider.Get(product.ProductGroupName) == null)
            {
                throw new NonexistentProductGroupException(product.DisplayName);
            }
            
            return Task.CompletedTask;
        }

        public virtual async Task<Product> UpdateAsync(Product product, IEnumerable<Guid> categoryIds = null)
        {
            await CheckProductGroupNameAsync(product);

            await CheckProductUniqueNameAsync(product);

            await _productRepository.UpdateAsync(product, autoSave: true);

            await CheckProductDetailAvailableAsync(product.Id, product.ProductDetailId);

            await UpdateProductCategoriesAsync(product.Id, categoryIds);

            return product;
        }

        public virtual async Task DeleteAsync(Product product)
        {
            await _productCategoryRepository.DeleteAsync(x => x.ProductId.Equals(product.Id));

            await _productRepository.DeleteAsync(product, true);
        }
        
        public virtual async Task DeleteAsync(Guid id)
        {
            await _productCategoryRepository.DeleteAsync(x => x.ProductId.Equals(id));

            await _productRepository.DeleteAsync(id, true);
        }

        public virtual async Task<Product> CreateSkuAsync(Product product, ProductSku productSku)
        {
            // productSku.SetSerializedAttributeOptionIds(await _attributeOptionIdsSerializer.FormatAsync(productSku.SerializedAttributeOptionIds));

            await CheckSkuAttributeOptionsAsync(product, productSku);

            await CheckProductSkuNameUniqueAsync(product, productSku);
            
            productSku.TrimCode();
            
            product.ProductSkus.AddIfNotContains(productSku);
            
            return await _productRepository.UpdateAsync(product, true);
        }

        protected virtual Task CheckProductSkuNameUniqueAsync(Product product, ProductSku productSku)
        {
            if (productSku.Name.IsNullOrEmpty())
            {
                return Task.CompletedTask;
            }
            
            if (product.ProductSkus.Where(sku => sku.Id != productSku.Id)
                .FirstOrDefault(sku => sku.Name == productSku.Name) != null)
            {
                throw new ProductSkuCodeDuplicatedException(product.Id, productSku.Name);
            }

            return Task.CompletedTask;
        }

        protected virtual Task CheckSkuAttributeOptionsAsync(Product product, ProductSku productSku)
        {
            if (product.ProductSkus.Where(sku => sku.Id != productSku.Id).FirstOrDefault(sku =>
                sku.SerializedAttributeOptionIds.Equals(productSku.SerializedAttributeOptionIds)) != null)
            {
                throw new ProductSkuDuplicatedException(product.Id, productSku.SerializedAttributeOptionIds);
            }

            return Task.CompletedTask;
        }

        public virtual async Task<Product> UpdateSkuAsync(Product product, ProductSku productSku)
        {
            await CheckProductSkuNameUniqueAsync(product, productSku);

            return await _productRepository.UpdateAsync(product, true);
        }

        public virtual async Task<Product> DeleteSkuAsync(Product product, ProductSku productSku)
        {
            product.ProductSkus.Remove(productSku);
            
            return await _productRepository.UpdateAsync(product, true);
        }

        protected virtual async Task CheckProductUniqueNameAsync(Product product)
        {
            await _productRepository.CheckUniqueNameAsync(product);
        }
        
        protected virtual async Task CheckProductDetailAvailableAsync(Guid currentProductId, Guid desiredProductDetailId)
        {
            var otherOwner = await _productRepository.FindAsync(x =>
                x.ProductDetailId == desiredProductDetailId && x.Id != currentProductId);

            // Todo: should also check ProductSku owner
            
            if (otherOwner != null)
            {
                throw new ProductDetailHasBeenUsedException(desiredProductDetailId);
            }
        }
        
        protected virtual async Task UpdateProductCategoriesAsync(Guid productId, IEnumerable<Guid> categoryIds)
        {
            await _productCategoryRepository.DeleteAsync(x => x.ProductId.Equals(productId));

            if (categoryIds == null)
            {
                return;
            }
            
            foreach (var categoryId in categoryIds)
            {
                await _productCategoryRepository.InsertAsync(
                    new ProductCategory(GuidGenerator.Create(), CurrentTenant.Id, categoryId, productId), true);
            }
        }

        public virtual async Task<bool> IsInventorySufficientAsync(Product product, ProductSku productSku, int quantity)
        {
            var inventoryData = await _productInventoryProvider.GetInventoryDataAsync(product, productSku);
            
            return product.InventoryStrategy == InventoryStrategy.NoNeed || inventoryData.Inventory - quantity >= 0;
        }

        public virtual async Task<InventoryDataModel> GetInventoryDataAsync(Product product, ProductSku productSku)
        {
            return await _productInventoryProvider.GetInventoryDataAsync(product, productSku);
        }

        public virtual async Task<bool> TryIncreaseInventoryAsync(Product product, ProductSku productSku, int quantity, bool reduceSold)
        {
            return await _productInventoryProvider.TryIncreaseInventoryAsync(product, productSku, quantity, reduceSold);
        }

        public virtual async Task<bool> TryReduceInventoryAsync(Product product, ProductSku productSku, int quantity, bool increaseSold)
        {
            return await _productInventoryProvider.TryReduceInventoryAsync(product, productSku, quantity, increaseSold);
        }

        public virtual async Task<PriceDataModel> GetProductPriceAsync(Product product, ProductSku productSku)
        {
            var price = await _productPriceProvider.GetPriceAsync(product, productSku);

            var discountedPrice = price;
            
            foreach (var provider in ServiceProvider.GetServices<IProductDiscountProvider>())
            {
                discountedPrice = await provider.GetDiscountedPriceAsync(product, productSku, discountedPrice);
            }

            return new PriceDataModel
            {
                Price = price,
                DiscountedPrice = discountedPrice
            };
        }
    }
}