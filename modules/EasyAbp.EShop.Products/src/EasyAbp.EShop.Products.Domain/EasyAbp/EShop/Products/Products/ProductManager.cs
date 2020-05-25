using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductCategories;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductStores;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductManager : DomainService, IProductManager
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductStoreRepository _productStoreRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductInventoryProvider _productInventoryProvider;

        public ProductManager(
            IProductRepository productRepository,
            IProductStoreRepository productStoreRepository,
            IProductCategoryRepository productCategoryRepository,
            IProductInventoryProvider productInventoryProvider)
        {
            _productRepository = productRepository;
            _productStoreRepository = productStoreRepository;
            _productCategoryRepository = productCategoryRepository;
            _productInventoryProvider = productInventoryProvider;
        }

        public virtual async Task<Product> CreateAsync(Product product, Guid? storeId = null,
            IEnumerable<Guid> categoryIds = null)
        {
            await CheckProductCodeUniqueAsync(product);

            await _productRepository.InsertAsync(product, autoSave: true);

            await CheckProductDetailAvailableAsync(product.Id, product.ProductDetailId);

            await UpdateProductCategoriesAsync(product.Id, categoryIds);

            if (storeId.HasValue)
            {
                await AddProductToStoreAsync(product.Id, storeId.Value);
            }

            return product;
        }
        
        public virtual async Task<Product> UpdateAsync(Product product, IEnumerable<Guid> categoryIds = null)
        {
            await CheckProductCodeUniqueAsync(product);

            await _productRepository.UpdateAsync(product, autoSave: true);

            await CheckProductDetailAvailableAsync(product.Id, product.ProductDetailId);

            await UpdateProductCategoriesAsync(product.Id, categoryIds);

            return product;
        }

        public virtual async Task DeleteAsync(Product product)
        {
            await _productCategoryRepository.DeleteAsync(x => x.ProductId.Equals(product.Id));

            await _productRepository.DeleteAsync(product);
        }
        
        public virtual async Task DeleteAsync(Guid id)
        {
            await _productCategoryRepository.DeleteAsync(x => x.ProductId.Equals(id));

            await _productRepository.DeleteAsync(id);
        }

        protected virtual async Task AddProductToStoreAsync(Guid productId, Guid storeId)
        {
            await _productStoreRepository.InsertAsync(new ProductStore(GuidGenerator.Create(), CurrentTenant.Id,
                storeId, productId, true), true);
        }
        
        protected virtual async Task CheckProductCodeUniqueAsync(Product product)
        {
            if (product.Code.IsNullOrEmpty())
            {
                return;
            }

            if (await _productRepository.FindAsync(x => x.Code == product.Code && x.Id != product.Id) != null)
            {
                throw new ProductCodeDuplicatedException(product.Code);
            }
        }
        
        protected virtual async Task CheckProductDetailAvailableAsync(Guid currentProductId, Guid desiredProductDetailId)
        {
            var otherOwner = await _productRepository.FindAsync(x =>
                x.ProductDetailId == desiredProductDetailId && x.Id != currentProductId);

            // Todo: should also check ProductSku owner
            
            if (otherOwner != null)
            {
                throw new EntityNotFoundException(typeof(ProductDetail), desiredProductDetailId);
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

        public virtual async Task<bool> IsInventorySufficientAsync(Product product, ProductSku productSku, Guid storeId, int quantity)
        {
            var inventory = await _productInventoryProvider.GetInventoryAsync(product, productSku, storeId);
            
            return product.InventoryStrategy == InventoryStrategy.NoNeed || inventory - quantity >= 0;
        }

        public virtual async Task<int> GetInventoryAsync(Product product, ProductSku productSku, Guid storeId)
        {
            return await _productInventoryProvider.GetInventoryAsync(product, productSku, storeId);
        }

        public virtual async Task<bool> TryIncreaseInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity)
        {
            return await _productInventoryProvider.TryIncreaseInventoryAsync(product, productSku, storeId, quantity);
        }

        public virtual async Task<bool> TryReduceInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity)
        {
            return await _productInventoryProvider.TryReduceInventoryAsync(product, productSku, storeId, quantity);
        }
    }
}