using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Authorization;
using EasyAbp.EShop.Products.ProductCategories;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.ProductStores;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAppService : CrudAppService<Product, ProductDto, Guid, GetProductListDto, CreateUpdateProductDto, CreateUpdateProductDto>,
        IProductAppService
    {
        protected override string CreatePolicyName { get; set; } = ProductsPermissions.Products.Create;
        protected override string DeletePolicyName { get; set; } = ProductsPermissions.Products.Delete;
        protected override string UpdatePolicyName { get; set; } = ProductsPermissions.Products.Update;
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = null;

        private readonly ISerializedAttributeOptionIdsFormatter _serializedAttributeOptionIdsFormatter;
        private readonly IProductStoreRepository _productStoreRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _repository;

        public ProductAppService(
            ISerializedAttributeOptionIdsFormatter serializedAttributeOptionIdsFormatter,
            IProductStoreRepository productStoreRepository,
            IProductCategoryRepository productCategoryRepository,
            IProductRepository repository) : base(repository)
        {
            _serializedAttributeOptionIdsFormatter = serializedAttributeOptionIdsFormatter;
            _productStoreRepository = productStoreRepository;
            _productCategoryRepository = productCategoryRepository;
            _repository = repository;
        }

        protected override IQueryable<Product> CreateFilteredQuery(GetProductListDto input)
        {
            var query = input.CategoryId.HasValue
                ? _repository.GetQueryable(input.StoreId, input.CategoryId.Value)
                : _repository.GetQueryable(input.StoreId);

            return input.ShowHidden ? query : query.Where(x => !x.IsHidden);
        }

        public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            await CheckCreatePolicyAsync();

            var product = MapToEntity(input);

            TryToSetTenantId(product);
            
            await UpdateProductAttributesAsync(product, input);

            await Repository.InsertAsync(product, autoSave: true);

            await CheckProductDetailIdAsync(product.Id, input.ProductDetailId);

            await AddProductToStoreAsync(product.Id, input.StoreId);
            
            await UpdateProductCategoriesAsync(product.Id, input.CategoryIds);

            return MapToGetOutputDto(product);
        }

        private async Task CheckProductDetailIdAsync(Guid currentProductId, Guid desiredProductDetailId)
        {
            var otherOwner = await _repository.FindAsync(x =>
                x.ProductDetailId == desiredProductDetailId && x.Id != currentProductId);

            // Todo: should also check ProductSku owner
            
            if (otherOwner != null)
            {
                throw new EntityNotFoundException(typeof(ProductDetail), desiredProductDetailId);
            }
        }

        protected virtual async Task AddProductToStoreAsync(Guid productId, Guid storeId)
        {
            await _productStoreRepository.InsertAsync(new ProductStore(GuidGenerator.Create(), CurrentTenant.Id,
                storeId, productId, true), true);
        }

        public override async Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            await CheckUpdatePolicyAsync();

            await CheckStoreIsProductOwnerAsync(id, input.StoreId);
            
            var product = await GetEntityByIdAsync(id);
            
            CheckProductIsNotStatic(product);
            
            MapToEntity(input, product);

            await UpdateProductAttributesAsync(product, input);

            await Repository.UpdateAsync(product, autoSave: true);

            await CheckProductDetailIdAsync(product.Id, input.ProductDetailId);

            await UpdateProductCategoriesAsync(product.Id, input.CategoryIds);

            return MapToGetOutputDto(product);
        }

        protected virtual async Task CheckStoreIsProductOwnerAsync(Guid productId, Guid storeId)
        {
            var productStore = await _productStoreRepository.GetAsync(productId, storeId);

            if (!productStore.IsOwner)
            {
                throw new StoreIsNotProductOwnerException(productId, storeId);
            }
        }

        protected virtual async Task UpdateProductAttributesAsync(Product product, CreateUpdateProductDto input)
        {
            foreach (var attributeDto in input.ProductAttributes)
            {
                var attribute = product.ProductAttributes.FirstOrDefault(a => a.DisplayName == attributeDto.DisplayName);
                
                if (attribute == null)
                {
                    attribute = new ProductAttribute(GuidGenerator.Create(),
                        attributeDto.DisplayName, attributeDto.Description);
                    
                    product.ProductAttributes.Add(attribute);
                }

                foreach (var optionDto in attributeDto.ProductAttributeOptions)
                {
                    var option = attribute.ProductAttributeOptions.FirstOrDefault(o => o.DisplayName == optionDto.DisplayName);

                    if (option == null)
                    {
                        option = new ProductAttributeOption(GuidGenerator.Create(),
                            optionDto.DisplayName, optionDto.Description);
                    
                        attribute.ProductAttributeOptions.Add(option);
                    }
                }

                var exceptOptionNames = attribute.ProductAttributeOptions.Select(o => o.DisplayName)
                    .Except(attributeDto.ProductAttributeOptions.Select(o => o.DisplayName));

                attribute.ProductAttributeOptions.RemoveAll(o => exceptOptionNames.Contains(o.DisplayName));
            }

            var exceptAttributeNames = product.ProductAttributes.Select(a => a.DisplayName)
                .Except(input.ProductAttributes.Select(a => a.DisplayName));

            product.ProductAttributes.RemoveAll(a => exceptAttributeNames.Contains(a.DisplayName));
        }

        [RemoteService(false)]
        public override async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override async Task<ProductDto> GetAsync(Guid id)
        {
            var dto = await base.GetAsync(id);

            dto.CategoryIds = (await _productCategoryRepository.GetListByProductIdAsync(dto.Id))
                .Select(x => x.CategoryId).ToList();
            
            return dto;
        }

        public override Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input)
        {
            if (input.ShowHidden)
            {
                AuthorizationService.CheckAsync(ProductsPermissions.Products.Default);
            }
            
            return base.GetListAsync(input);
        }

        public async Task DeleteAsync(Guid id, Guid storeId)
        {
            await CheckDeletePolicyAsync();

            var product = await GetEntityByIdAsync(id);
            
            CheckProductIsNotStatic(product);
            
            await _productCategoryRepository.DeleteAsync(x => x.ProductId.Equals(id));
            
            await CheckStoreIsProductOwnerAsync(id, storeId);

            await _repository.DeleteAsync(product);
        }

        private static void CheckProductIsNotStatic(Product product)
        {
            if (product.IsStatic)
            {
                throw new StaticProductCannotBeModifiedException(product.Id);
            }
        }

        public async Task<ProductDto> CreateSkuAsync(Guid productId, Guid storeId, CreateProductSkuDto input)
        {
            await CheckUpdatePolicyAsync();
            
            await CheckStoreIsProductOwnerAsync(productId, storeId);
            
            var product = await GetEntityByIdAsync(productId);

            CheckProductIsNotStatic(product);

            input.SerializedAttributeOptionIds =
                await _serializedAttributeOptionIdsFormatter.ParseAsync(input.SerializedAttributeOptionIds);

            await CheckSkuAttributeOptionsAsync(product, input.SerializedAttributeOptionIds);

            var sku = ObjectMapper.Map<CreateProductSkuDto, ProductSku>(input);
            
            EntityHelper.TrySetId(sku, GuidGenerator.Create);
            
            product.ProductSkus.Add(sku);

            await _repository.UpdateAsync(product, true);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        protected virtual Task CheckSkuAttributeOptionsAsync(Product product, string inputSerializedAttributeOptionIds)
        {
            if (product.ProductSkus.FirstOrDefault(sku =>
                sku.SerializedAttributeOptionIds.Equals(inputSerializedAttributeOptionIds)) != null)
            {
                throw new ProductSkuDuplicatedException(product.Id, inputSerializedAttributeOptionIds);
            }

            return Task.CompletedTask;
        }

        public async Task<ProductDto> UpdateSkuAsync(Guid productId, Guid productSkuId, Guid storeId, UpdateProductSkuDto input)
        {
            await CheckUpdatePolicyAsync();
            
            await CheckStoreIsProductOwnerAsync(productId, storeId);
            
            var product = await GetEntityByIdAsync(productId);

            CheckProductIsNotStatic(product);

            var sku = product.ProductSkus.Single(x => x.Id == productSkuId);

            ObjectMapper.Map(input, sku);

            await _repository.UpdateAsync(product, true);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        public async Task<ProductDto> DeleteSkuAsync(Guid productId, Guid productSkuId, Guid storeId)
        {
            await CheckUpdatePolicyAsync();
            
            await CheckStoreIsProductOwnerAsync(productId, storeId);
            
            var product = await GetEntityByIdAsync(productId);
            
            CheckProductIsNotStatic(product);

            var sku = product.ProductSkus.Single(x => x.Id == productSkuId);

            product.ProductSkus.Remove(sku);
            
            await _repository.UpdateAsync(product, true);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        protected virtual async Task UpdateProductCategoriesAsync(Guid productId, IEnumerable<Guid> categoryIds)
        {
            await _productCategoryRepository.DeleteAsync(x => x.ProductId.Equals(productId));

            foreach (var categoryId in categoryIds)
            {
                await _productCategoryRepository.InsertAsync(
                    new ProductCategory(GuidGenerator.Create(), CurrentTenant.Id, categoryId, productId), true);
            }
        }
    }
}