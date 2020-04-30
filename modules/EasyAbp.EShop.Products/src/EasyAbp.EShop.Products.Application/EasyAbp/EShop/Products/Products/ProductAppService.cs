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

        private readonly IProductPurchasableStatusProvider _productPurchasableStatusProvider;
        private readonly IAttributeOptionIdsSerializer _attributeOptionIdsSerializer;
        private readonly IProductStoreRepository _productStoreRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _repository;

        public ProductAppService(
            IProductPurchasableStatusProvider productPurchasableStatusProvider,
            IAttributeOptionIdsSerializer attributeOptionIdsSerializer,
            IProductStoreRepository productStoreRepository,
            IProductCategoryRepository productCategoryRepository,
            IProductRepository repository) : base(repository)
        {
            _productPurchasableStatusProvider = productPurchasableStatusProvider;
            _attributeOptionIdsSerializer = attributeOptionIdsSerializer;
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
            var isProductSkusEmpty = product.ProductSkus.IsNullOrEmpty();

            var usedAttributeOptionIds = new HashSet<Guid>();
            
            foreach (var serializedAttributeOptionIds in product.ProductSkus.Select(sku => sku.SerializedAttributeOptionIds))
            {
                foreach (var attributeOptionId in await _attributeOptionIdsSerializer.DeserializeAsync(serializedAttributeOptionIds))
                {
                    usedAttributeOptionIds.Add(attributeOptionId);
                }
            }
            
            foreach (var attributeDto in input.ProductAttributes)
            {
                var attribute = product.ProductAttributes.FirstOrDefault(a => a.DisplayName == attributeDto.DisplayName);
                
                if (attribute == null)
                {
                    if (!isProductSkusEmpty)
                    {
                        throw new ProductAttributesModificationFailedException();
                    }
                    
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

                var removedOptionNames = attribute.ProductAttributeOptions.Select(o => o.DisplayName)
                    .Except(attributeDto.ProductAttributeOptions.Select(o => o.DisplayName)).ToList();

                if (!isProductSkusEmpty && removedOptionNames.Any() && usedAttributeOptionIds
                    .Intersect(attribute.ProductAttributeOptions
                        .Where(option => removedOptionNames.Contains(option.DisplayName))
                        .Select(option => option.Id)).Any())
                {
                    throw new ProductAttributeOptionsDeletionFailedException();
                }

                attribute.ProductAttributeOptions.RemoveAll(o => removedOptionNames.Contains(o.DisplayName));
            }

            var removedAttributeNames = product.ProductAttributes.Select(a => a.DisplayName)
                .Except(input.ProductAttributes.Select(a => a.DisplayName)).ToList();

            if (!isProductSkusEmpty && removedAttributeNames.Any())
            {
                throw new ProductAttributesModificationFailedException();
            }
            
            product.ProductAttributes.RemoveAll(a => removedAttributeNames.Contains(a.DisplayName));
        }

        [Obsolete("Should use DeleteAsync(Guid id, Guid storeId)")]
        [RemoteService(false)]
        public override Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [Obsolete("Should use GetAsync(Guid id, Guid storeId)")]
        [RemoteService(false)]
        public override Task<ProductDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        
        public virtual async Task<ProductDto> GetAsync(Guid id, Guid storeId)
        {
            var dto = await base.GetAsync(id);

            if (!dto.IsPublished)
            {
                await CheckStoreIsProductOwnerAsync(id, storeId);
            }

            dto.CategoryIds = (await _productCategoryRepository.GetListByProductIdAsync(dto.Id))
                .Select(x => x.CategoryId).ToList();
            
            return dto;
        }

        public override async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input)
        {
            await CheckGetListPolicyAsync();

            // Todo: Check if current user is an admin of the store.
            var isCurrentUserStoreAdmin = true && await AuthorizationService.IsGrantedAsync(ProductsPermissions.Products.Default);
            
            if (input.ShowHidden && !isCurrentUserStoreAdmin)
            {
                throw new NotAllowedToGetProductListWithShowHiddenException();
            }

            var query = CreateFilteredQuery(input);
            
            if (!isCurrentUserStoreAdmin)
            {
                query = query.Where(x => x.IsPublished);
            }

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<ProductDto>(
                totalCount,
                entities.Select(MapToGetListOutputDto).ToList()
            );
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
                await _attributeOptionIdsSerializer.FormatAsync(input.SerializedAttributeOptionIds);

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

        public async Task<GetProductPurchasableStatusResult> GetPurchasableStatusAsync(Guid productId, Guid productSkuId, Guid storeId)
        {
            var product = await _repository.GetAsync(productId);

            var productSku = product.ProductSkus.Single(sku => sku.Id == productSkuId);

            return await _productPurchasableStatusProvider.GetPurchasableStatusAsync(product, productSku, storeId);
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