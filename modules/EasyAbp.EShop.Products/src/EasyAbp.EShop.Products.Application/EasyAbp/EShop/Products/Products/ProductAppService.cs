using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Options;
using EasyAbp.EShop.Products.ProductInventories;
using EasyAbp.EShop.Products.Products.CacheItems;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAppService :
        MultiStoreCrudAppService<Product, ProductDto, Guid, GetProductListInput, CreateUpdateProductDto,
            CreateUpdateProductDto>, IProductAppService
    {
        protected override string CreatePolicyName { get; set; } = ProductsPermissions.Products.Create;
        protected override string DeletePolicyName { get; set; } = ProductsPermissions.Products.Delete;
        protected override string UpdatePolicyName { get; set; } = ProductsPermissions.Products.Update;
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = null;
        protected override string CrossStorePolicyName { get; set; } = ProductsPermissions.Products.CrossStore;

        private readonly IProductManager _productManager;
        private readonly IDistributedCache<ProductViewCacheItem> _cache;
        private readonly EShopProductsOptions _options;
        private readonly IProductInventoryProvider _productInventoryProvider;
        private readonly IProductViewCacheKeyProvider _productViewCacheKeyProvider;
        private readonly IAttributeOptionIdsSerializer _attributeOptionIdsSerializer;
        private readonly IProductRepository _repository;

        public ProductAppService(
            IProductManager productManager,
            IOptions<EShopProductsOptions> options,
            IDistributedCache<ProductViewCacheItem> cache,
            IProductInventoryProvider productInventoryProvider,
            IProductViewCacheKeyProvider productViewCacheKeyProvider,
            IAttributeOptionIdsSerializer attributeOptionIdsSerializer,
            IProductRepository repository) : base(repository)
        {
            _productManager = productManager;
            _cache = cache;
            _options = options.Value;
            _productInventoryProvider = productInventoryProvider;
            _productViewCacheKeyProvider = productViewCacheKeyProvider;
            _attributeOptionIdsSerializer = attributeOptionIdsSerializer;
            _repository = repository;
        }

        protected override async Task<IQueryable<Product>> CreateFilteredQueryAsync(GetProductListInput input)
        {
            var query = input.CategoryId.HasValue
                ? await _repository.WithDetailsAsync(input.CategoryId.Value)
                : (await _repository.WithDetailsAsync());

            return query
                .Where(x => x.StoreId == input.StoreId)
                .WhereIf(!input.ShowHidden, x => !x.IsHidden)
                .WhereIf(!input.ShowUnpublished, x => x.IsPublished);
        }

        protected override Product MapToEntity(CreateUpdateProductDto createInput)
        {
            var product = base.MapToEntity(createInput);

            return product;
        }

        public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            var product = MapToEntity(input);

            await CheckMultiStorePolicyAsync(product.StoreId, CreatePolicyName);

            TryToSetTenantId(product);

            await UpdateProductAttributesAsync(product, input);

            await _productManager.CreateAsync(product, input.CategoryIds);

            var dto = await MapToGetOutputDtoAsync(product);

            await LoadDtoExtraDataAsync(product, dto);
            await LoadDtosProductGroupDisplayNameAsync(new[] { dto });

            UnitOfWorkManager.Current.OnCompleted(async () => { await ClearProductViewCacheAsync(product.StoreId); });

            return dto;
        }

        protected virtual async Task ClearProductViewCacheAsync(Guid storeId)
        {
            await _cache.RemoveAsync(await _productViewCacheKeyProvider.GetCacheKeyAsync(storeId));
        }

        public override async Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            var product = await GetEntityByIdAsync(id);

            await CheckMultiStorePolicyAsync(product.StoreId, UpdatePolicyName);

            if (input.StoreId != product.StoreId)
            {
                await CheckMultiStorePolicyAsync(input.StoreId, UpdatePolicyName);
            }

            CheckProductIsNotStatic(product);

            MapToEntity(input, product);

            await UpdateProductAttributesAsync(product, input);

            await _productManager.UpdateAsync(product, input.CategoryIds);

            var dto = await MapToGetOutputDtoAsync(product);

            await LoadDtoExtraDataAsync(product, dto);
            await LoadDtosProductGroupDisplayNameAsync(new[] { dto });

            UnitOfWorkManager.Current.OnCompleted(async () => { await ClearProductViewCacheAsync(product.StoreId); });

            return dto;
        }

        protected virtual async Task UpdateProductAttributesAsync(Product product, CreateUpdateProductDto input)
        {
            var isProductSkusEmpty = product.ProductSkus.IsNullOrEmpty();

            var usedAttributeOptionIds = new HashSet<Guid>();

            foreach (var serializedAttributeOptionIds in product.ProductSkus.Select(sku =>
                         sku.SerializedAttributeOptionIds))
            {
                foreach (var attributeOptionId in await _attributeOptionIdsSerializer.DeserializeAsync(
                             serializedAttributeOptionIds))
                {
                    usedAttributeOptionIds.Add(attributeOptionId);
                }
            }

            foreach (var attributeDto in input.ProductAttributes)
            {
                var attribute =
                    product.ProductAttributes.FirstOrDefault(a => a.DisplayName == attributeDto.DisplayName);

                if (attribute == null)
                {
                    if (!isProductSkusEmpty)
                    {
                        throw new ProductAttributesModificationFailedException();
                    }

                    attribute = new ProductAttribute(GuidGenerator.Create(),
                        attributeDto.DisplayName, attributeDto.Description, attributeDto.DisplayOrder);

                    product.ProductAttributes.Add(attribute);
                }

                foreach (var optionDto in attributeDto.ProductAttributeOptions)
                {
                    var option =
                        attribute.ProductAttributeOptions.FirstOrDefault(o => o.DisplayName == optionDto.DisplayName);

                    if (option == null)
                    {
                        option = new ProductAttributeOption(GuidGenerator.Create(),
                            optionDto.DisplayName, optionDto.Description, optionDto.DisplayOrder);

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

        public override async Task<ProductDto> GetAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            var product = await GetEntityByIdAsync(id);

            if (!product.IsPublished)
            {
                await CheckMultiStorePolicyAsync(product.StoreId, ProductsPermissions.Products.Manage);
            }

            var dto = await MapToGetOutputDtoAsync(product);

            await LoadDtoExtraDataAsync(product, dto);
            await LoadDtosProductGroupDisplayNameAsync(new[] { dto });

            return dto;
        }

        protected virtual Task LoadDtosProductGroupDisplayNameAsync(IEnumerable<ProductDto> dtos)
        {
            var dict = _options.Groups.GetConfigurationsDictionary();

            foreach (var dto in dtos)
            {
                dto.ProductGroupDisplayName = dict[dto.ProductGroupName].DisplayName;
            }

            return Task.CompletedTask;
        }

        public virtual async Task<ProductDto> GetByUniqueNameAsync(Guid storeId, string uniqueName)
        {
            await CheckGetPolicyAsync();

            var product = await _repository.GetAsync(x => x.UniqueName == uniqueName);

            if (!product.IsPublished)
            {
                await CheckMultiStorePolicyAsync(product.StoreId, ProductsPermissions.Products.Manage);
            }

            var dto = await MapToGetOutputDtoAsync(product);

            await LoadDtoExtraDataAsync(product, dto);
            await LoadDtosProductGroupDisplayNameAsync(new[] { dto });

            return dto;
        }

        public override async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListInput input)
        {
            await CheckGetListPolicyAsync();

            if (input.ShowHidden || input.ShowUnpublished)
            {
                await CheckMultiStorePolicyAsync(input.StoreId, ProductsPermissions.Products.Manage);
            }

            // Todo: Products cache.
            var query = await CreateFilteredQueryAsync(input);

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var products = await AsyncExecuter.ToListAsync(query);

            var items = new List<ProductDto>();

            foreach (var product in products)
            {
                var productDto = await MapToGetListOutputDtoAsync(product);

                await LoadDtoExtraDataAsync(product, productDto);

                items.Add(productDto);
            }

            await LoadDtosProductGroupDisplayNameAsync(items);

            return new PagedResultDto<ProductDto>(totalCount, items);
        }

        protected virtual async Task<ProductDto> LoadDtoInventoryDataAsync(Product product, ProductDto productDto)
        {
            var models = product.ProductSkus.Select(x =>
                new InventoryQueryModel(product.TenantId, product.StoreId, product.Id, x.Id)).ToList();

            var inventoryDataDict = await _productInventoryProvider.GetSkuIdInventoryDataMappingAsync(models);

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

        protected virtual async Task<ProductDto> LoadDtoExtraDataAsync(Product product, ProductDto productDto)
        {
            await LoadDtoInventoryDataAsync(product, productDto);
            await LoadDtoPriceDataAsync(product, productDto);

            return productDto;
        }

        protected virtual async Task<ProductDto> LoadDtoPriceDataAsync(Product product, ProductDto productDto)
        {
            foreach (var productSku in product.ProductSkus)
            {
                var productSkuDto = productDto.ProductSkus.First(x => x.Id == productSku.Id);

                var priceDataModel = await _productManager.GetRealPriceAsync(product, productSku);

                productSkuDto.Price = priceDataModel.Price;
                productSkuDto.DiscountedPrice = priceDataModel.DiscountedPrice;
            }

            if (productDto.ProductSkus.Count > 0)
            {
                productDto.MinimumPrice = productDto.ProductSkus.Min(sku => sku.Price);
                productDto.MaximumPrice = productDto.ProductSkus.Max(sku => sku.Price);
            }

            return productDto;
        }

        public override async Task DeleteAsync(Guid id)
        {
            var product = await GetEntityByIdAsync(id);

            await CheckMultiStorePolicyAsync(product.StoreId, DeletePolicyName);

            CheckProductIsNotStatic(product);

            await _productManager.DeleteAsync(product);

            UnitOfWorkManager.Current.OnCompleted(async () => { await ClearProductViewCacheAsync(product.StoreId); });
        }

        private static void CheckProductIsNotStatic(Product product)
        {
            if (product.IsStatic)
            {
                throw new StaticProductCannotBeModifiedException(product.Id);
            }
        }

        public async Task<ProductDto> CreateSkuAsync(Guid productId, CreateProductSkuDto input)
        {
            var product = await GetEntityByIdAsync(productId);

            await CheckMultiStorePolicyAsync(product.StoreId, UpdatePolicyName);

            CheckProductIsNotStatic(product);

            var sku = ObjectMapper.Map<CreateProductSkuDto, ProductSku>(input);

            EntityHelper.TrySetId(sku, GuidGenerator.Create);

            await _productManager.CreateSkuAsync(product, sku);

            var dto = await MapToGetOutputDtoAsync(product);

            await LoadDtoExtraDataAsync(product, dto);
            await LoadDtosProductGroupDisplayNameAsync(new[] { dto });

            UnitOfWorkManager.Current.OnCompleted(async () => { await ClearProductViewCacheAsync(product.StoreId); });

            return dto;
        }

        public async Task<ProductDto> UpdateSkuAsync(Guid productId, Guid productSkuId, UpdateProductSkuDto input)
        {
            var product = await GetEntityByIdAsync(productId);

            await CheckMultiStorePolicyAsync(product.StoreId, UpdatePolicyName);

            CheckProductIsNotStatic(product);

            var sku = product.ProductSkus.Single(x => x.Id == productSkuId);

            ObjectMapper.Map(input, sku);

            await _productManager.UpdateSkuAsync(product, sku);

            var dto = await MapToGetOutputDtoAsync(product);

            await LoadDtoExtraDataAsync(product, dto);
            await LoadDtosProductGroupDisplayNameAsync(new[] { dto });

            UnitOfWorkManager.Current.OnCompleted(async () => { await ClearProductViewCacheAsync(product.StoreId); });

            return dto;
        }

        public async Task<ProductDto> DeleteSkuAsync(Guid productId, Guid productSkuId)
        {
            var product = await GetEntityByIdAsync(productId);

            await CheckMultiStorePolicyAsync(product.StoreId, UpdatePolicyName);

            CheckProductIsNotStatic(product);

            var sku = product.ProductSkus.Single(x => x.Id == productSkuId);

            await _productManager.DeleteSkuAsync(product, sku);

            var dto = await MapToGetOutputDtoAsync(product);

            await LoadDtoExtraDataAsync(product, dto);
            await LoadDtosProductGroupDisplayNameAsync(new[] { dto });

            UnitOfWorkManager.Current.OnCompleted(async () => { await ClearProductViewCacheAsync(product.StoreId); });

            return dto;
        }

        public virtual Task<ListResultDto<ProductGroupDto>> GetProductGroupListAsync()
        {
            var dict = _options.Groups.GetConfigurationsDictionary();

            return Task.FromResult(new ListResultDto<ProductGroupDto>(dict.Select(x =>
                new ProductGroupDto
                {
                    Name = x.Key,
                    DisplayName = x.Value.DisplayName,
                    Description = x.Value.Description
                }
            ).ToList()));
        }

        protected override ProductDto MapToGetOutputDto(Product entity)
        {
            var productDto = base.MapToGetOutputDto(entity);

            return SortAttributesAndOptions(productDto);
        }

        protected override ProductDto MapToGetListOutputDto(Product entity)
        {
            var productDto = base.MapToGetListOutputDto(entity);

            return SortAttributesAndOptions(productDto);
        }

        protected virtual ProductDto SortAttributesAndOptions(ProductDto productDto)
        {
            productDto.ProductAttributes = productDto.ProductAttributes.OrderByDescending(x => x.DisplayOrder).ToList();

            foreach (var productAttributeDto in productDto.ProductAttributes)
            {
                productAttributeDto.ProductAttributeOptions = productAttributeDto.ProductAttributeOptions
                    .OrderByDescending(x => x.DisplayOrder).ToList();
            }

            return productDto;
        }
    }
}