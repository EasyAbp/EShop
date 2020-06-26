using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.ProductStores;
using EasyAbp.EShop.Products.ProductTypes;
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

        private readonly IProductManager _productManager;
        private readonly IProductInventoryProvider _productInventoryProvider;
        private readonly IAttributeOptionIdsSerializer _attributeOptionIdsSerializer;
        private readonly IProductStoreRepository _productStoreRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IProductRepository _repository;

        public ProductAppService(
            IProductManager productManager,
            IProductInventoryProvider productInventoryProvider,
            IAttributeOptionIdsSerializer attributeOptionIdsSerializer,
            IProductStoreRepository productStoreRepository,
            IProductTypeRepository productTypeRepository,
            IProductRepository repository) : base(repository)
        {
            _productManager = productManager;
            _productInventoryProvider = productInventoryProvider;
            _attributeOptionIdsSerializer = attributeOptionIdsSerializer;
            _productStoreRepository = productStoreRepository;
            _productTypeRepository = productTypeRepository;
            _repository = repository;
        }

        protected override IQueryable<Product> CreateFilteredQuery(GetProductListDto input)
        {
            var query = _repository.WithDetails(input.StoreId, input.CategoryId, input.TagId);

            return input.ShowHidden ? query : query.Where(x => !x.IsHidden);
        }

        protected override Product MapToEntity(CreateUpdateProductDto createInput)
        {
            var product = base.MapToEntity(createInput);

            return product;
        }

        public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            await CheckCreatePolicyAsync();

            var product = MapToEntity(input);

            TryToSetTenantId(product);

            await UpdateProductAttributesAsync(product, input);

            await _productManager.CreateAsync(product, input.StoreId, input.CategoryIds, input.TagIds);

            return MapToGetOutputDto(product);
        }

        public override async Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            await CheckUpdatePolicyAsync();

            await CheckStoreIsProductOwnerAsync(id, input.StoreId);

            var product = await GetEntityByIdAsync(id);

            CheckProductIsNotStatic(product);

            MapToEntity(input, product);

            await UpdateProductAttributesAsync(product, input);

            await _productManager.UpdateAsync(product, input.CategoryIds, input.TagIds);

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
            throw new NotSupportedException();
        }

        [Obsolete("Should use GetAsync(Guid id, Guid storeId)")]
        [RemoteService(false)]
        public override Task<ProductDto> GetAsync(Guid id)
        {
            throw new NotSupportedException();
        }

        public virtual async Task<ProductDto> GetAsync(Guid id, Guid storeId)
        {
            await CheckGetPolicyAsync();

            var product = await GetEntityByIdAsync(id);

            if (!product.IsPublished)
            {
                await CheckStoreIsProductOwnerAsync(product.Id, storeId);
            }

            var dto = MapToGetOutputDto(product);

            await LoadDtoInventoryDataAsync(product, dto, storeId);
            await LoadDtoPriceAsync(product, dto, storeId);

            await LoadDtosProductTypeUniqueNameAsync(new[] {dto});

            return dto;
        }

        protected virtual async Task LoadDtosProductTypeUniqueNameAsync(IEnumerable<ProductDto> dtos)
        {
            var dict = (await _productTypeRepository.GetListAsync()).ToDictionary(x => x.Id, x => x.UniqueName);

            foreach (var dto in dtos)
            {
                dto.ProductTypeUniqueName = dict[dto.ProductTypeId];
            }
        }

        public virtual async Task<ProductDto> GetByCodeAsync(string code, Guid storeId)
        {
            await CheckGetPolicyAsync();

            var product = await _repository.GetAsync(x => x.UniqueName == code);

            if (!product.IsPublished)
            {
                await CheckStoreIsProductOwnerAsync(product.Id, storeId);
            }

            var dto = MapToGetOutputDto(product);

            await LoadDtoInventoryDataAsync(product, dto, storeId);

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

            // Todo: Products cache.
            var query = CreateFilteredQuery(input);

            if (!isCurrentUserStoreAdmin)
            {
                query = query.Where(x => x.IsPublished);
            }

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var products = await AsyncExecuter.ToListAsync(query);

            var items = new List<ProductDto>();

            foreach (var product in products)
            {
                var productDto = MapToGetListOutputDto(product);

                await LoadDtoInventoryDataAsync(product, productDto, input.StoreId);
                await LoadDtoPriceAsync(product, productDto, input.StoreId);

                items.Add(productDto);
            }

            await LoadDtosProductTypeUniqueNameAsync(items);
            
            return new PagedResultDto<ProductDto>(totalCount, items);
        }

        protected virtual async Task<ProductDto> LoadDtoInventoryDataAsync(Product product, ProductDto productDto, Guid storeId)
        {
            var inventoryDataDict = await _productInventoryProvider.GetInventoryDataDictionaryAsync(product, storeId);

            productDto.Sold = 0;
            
            foreach (var productSkuDto in productDto.ProductSkus)
            {
                var inventoryData = inventoryDataDict[productSkuDto.Id];
                
                productSkuDto.Inventory = inventoryData.Inventory;
                productSkuDto.Sold = inventoryData.Sold;
                productDto.Sold += productSkuDto.Sold;
            }

            return productDto;
        }

        protected virtual async Task<ProductDto> LoadDtoPriceAsync(Product product, ProductDto productDto, Guid storeId)
        {
            foreach (var productSkuDto in productDto.ProductSkus)
            {
                productSkuDto.DiscountedPrice = await _productManager.GetDiscountedPriceAsync(product,
                    product.ProductSkus.Single(sku => sku.Id == productSkuDto.Id), storeId);
            }

            if (productDto.ProductSkus.Count > 0)
            {
                productDto.MinimumPrice = productDto.ProductSkus.Min(sku => sku.Price);
                productDto.MaximumPrice = productDto.ProductSkus.Max(sku => sku.Price);
            }

            return productDto;
        }

        public async Task DeleteAsync(Guid id, Guid storeId)
        {
            await CheckDeletePolicyAsync();

            var product = await GetEntityByIdAsync(id);

            CheckProductIsNotStatic(product);

            await CheckStoreIsProductOwnerAsync(id, storeId);

            await _productManager.DeleteAsync(product);
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

            var sku = ObjectMapper.Map<CreateProductSkuDto, ProductSku>(input);

            EntityHelper.TrySetId(sku, GuidGenerator.Create);

            await _productManager.CreateSkuAsync(product, sku);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        public async Task<ProductDto> UpdateSkuAsync(Guid productId, Guid productSkuId, Guid storeId, UpdateProductSkuDto input)
        {
            await CheckUpdatePolicyAsync();

            await CheckStoreIsProductOwnerAsync(productId, storeId);

            var product = await GetEntityByIdAsync(productId);

            CheckProductIsNotStatic(product);

            var sku = product.ProductSkus.Single(x => x.Id == productSkuId);

            ObjectMapper.Map(input, sku);

            await _productManager.UpdateSkuAsync(product, sku);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        public async Task<ProductDto> DeleteSkuAsync(Guid productId, Guid productSkuId, Guid storeId)
        {
            await CheckUpdatePolicyAsync();

            await CheckStoreIsProductOwnerAsync(productId, storeId);

            var product = await GetEntityByIdAsync(productId);

            CheckProductIsNotStatic(product);

            var sku = product.ProductSkus.Single(x => x.Id == productSkuId);

            await _productManager.DeleteSkuAsync(product, sku);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }
    }
}