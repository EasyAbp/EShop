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
using Volo.Abp.ObjectExtending;

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
        private readonly IProductInventoryProviderResolver _productInventoryProviderResolver;
        private readonly IProductViewCacheKeyProvider _productViewCacheKeyProvider;
        private readonly IProductRepository _repository;

        public ProductAppService(
            IProductManager productManager,
            IOptions<EShopProductsOptions> options,
            IDistributedCache<ProductViewCacheItem> cache,
            IProductInventoryProviderResolver productInventoryProviderResolver,
            IProductViewCacheKeyProvider productViewCacheKeyProvider,
            IProductRepository repository) : base(repository)
        {
            _productManager = productManager;
            _cache = cache;
            _options = options.Value;
            _productInventoryProviderResolver = productInventoryProviderResolver;
            _productViewCacheKeyProvider = productViewCacheKeyProvider;
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

        protected override IQueryable<Product> ApplyDefaultSorting(IQueryable<Product> query)
        {
            return query.OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Id);
        }

        public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            var product = await MapToEntityAsync(input);

            await CheckMultiStorePolicyAsync(product.StoreId, CreatePolicyName);

            TryToSetTenantId(product);

            await UpdateProductAttributesAsync(product, input);

            await _productManager.CreateAsync(product, input.CategoryIds);

            var dto = await MapToGetOutputDtoAsync(product);

            await LoadDtoExtraDataAsync(product, dto, Clock.Now);
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

            await MapToEntityAsync(input, product);

            await UpdateProductAttributesAsync(product, input);

            await _productManager.UpdateAsync(product, input.CategoryIds);

            var dto = await MapToGetOutputDtoAsync(product);

            await LoadDtoExtraDataAsync(product, dto, Clock.Now);
            await LoadDtosProductGroupDisplayNameAsync(new[] { dto });

            UnitOfWorkManager.Current.OnCompleted(async () => { await ClearProductViewCacheAsync(product.StoreId); });

            return dto;
        }

        protected virtual Task UpdateProductAttributesAsync(Product product, CreateUpdateProductDto input)
        {
            var isProductSkusEmpty = product.ProductSkus.IsNullOrEmpty();

            var usedAttributeOptionIds = new HashSet<Guid>(product.ProductSkus.SelectMany(x => x.AttributeOptionIds));

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
            return Task.CompletedTask;
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

            await LoadDtoExtraDataAsync(product, dto, Clock.Now);
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

            await LoadDtoExtraDataAsync(product, dto, Clock.Now);
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

            var now = Clock.Now;
            var items = new List<ProductDto>();

            foreach (var product in products)
            {
                var productDto = await MapToGetListOutputDtoAsync(product);

                await LoadDtoExtraDataAsync(product, productDto, now);

                items.Add(productDto);
            }

            await LoadDtosProductGroupDisplayNameAsync(items);

            return new PagedResultDto<ProductDto>(totalCount, items);
        }

        protected virtual async Task<ProductDto> LoadDtoInventoryDataAsync(Product product, ProductDto productDto)
        {
            var models = product.ProductSkus.Select(x =>
                new InventoryQueryModel(product.TenantId, product.StoreId, product.Id, x.Id)).ToList();

            var inventoryProvider = await _productInventoryProviderResolver.GetAsync(product);

            var inventoryDataDict = await inventoryProvider.GetSkuIdInventoryDataMappingAsync(models);

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

        protected virtual async Task<ProductDto> LoadDtoExtraDataAsync(Product product, ProductDto productDto,
            DateTime now)
        {
            await LoadDtoInventoryDataAsync(product, productDto);
            await LoadDtoPriceDataAsync(product, productDto, now);

            return productDto;
        }

        protected virtual async Task<ProductDto> LoadDtoPriceDataAsync(Product product, ProductDto productDto,
            DateTime now)
        {
            foreach (var productSku in product.ProductSkus)
            {
                var productSkuDto = productDto.ProductSkus.First(x => x.Id == productSku.Id);

                var realTimePriceInfoModel = await _productManager.GetRealTimePriceAsync(product, productSku, now);

                productSkuDto.PriceWithoutDiscount = realTimePriceInfoModel.PriceWithoutDiscount;
                productSkuDto.Price = realTimePriceInfoModel.TotalDiscountedPrice;
                productSkuDto.ProductDiscounts = realTimePriceInfoModel.Discounts.ProductDiscounts;
                productSkuDto.OrderDiscountPreviews = realTimePriceInfoModel.Discounts.OrderDiscountPreviews;
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

        protected virtual void CheckProductIsNotStatic(Product product)
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

            var sku = await MapToProductSkuAsync(input);

            EntityHelper.TrySetId(sku, GuidGenerator.Create);

            await _productManager.CreateSkuAsync(product, sku);

            var dto = await MapToGetOutputDtoAsync(product);

            await LoadDtoExtraDataAsync(product, dto, Clock.Now);
            await LoadDtosProductGroupDisplayNameAsync(new[] { dto });

            UnitOfWorkManager.Current.OnCompleted(async () => { await ClearProductViewCacheAsync(product.StoreId); });

            return dto;
        }

        public virtual async Task<ProductDto> UpdateSkuAsync(Guid productId, Guid productSkuId,
            UpdateProductSkuDto input)
        {
            var product = await GetEntityByIdAsync(productId);

            await CheckMultiStorePolicyAsync(product.StoreId, UpdatePolicyName);

            CheckProductIsNotStatic(product);

            var sku = product.ProductSkus.Single(x => x.Id == productSkuId);

            await MapToProductSkuAsync(input, sku);

            await _productManager.UpdateSkuAsync(product, sku);

            var dto = await MapToGetOutputDtoAsync(product);

            await LoadDtoExtraDataAsync(product, dto, Clock.Now);
            await LoadDtosProductGroupDisplayNameAsync(new[] { dto });

            UnitOfWorkManager.Current.OnCompleted(async () => { await ClearProductViewCacheAsync(product.StoreId); });

            return dto;
        }

        public virtual async Task<ProductDto> DeleteSkuAsync(Guid productId, Guid productSkuId)
        {
            var product = await GetEntityByIdAsync(productId);

            await CheckMultiStorePolicyAsync(product.StoreId, UpdatePolicyName);

            CheckProductIsNotStatic(product);

            var sku = product.ProductSkus.Single(x => x.Id == productSkuId);

            await _productManager.DeleteSkuAsync(product, sku);

            var dto = await MapToGetOutputDtoAsync(product);

            await LoadDtoExtraDataAsync(product, dto, Clock.Now);
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

        public virtual async Task<ChangeProductInventoryResultDto> ChangeInventoryAsync(Guid id, Guid productSkuId,
            ChangeProductInventoryDto input)
        {
            var product = await GetEntityByIdAsync(id);
            var sku = product.ProductSkus.Single(x => x.Id == productSkuId);

            var changed = input.ChangedInventory switch
            {
                > 0 => await _productManager.TryIncreaseInventoryAsync(product, sku, input.ChangedInventory, false),
                < 0 => await _productManager.TryReduceInventoryAsync(product, sku, -1 * input.ChangedInventory, false),
                _ => false
            };

            var model = await _productManager.GetInventoryDataAsync(product, sku);

            return new ChangeProductInventoryResultDto
            {
                Changed = changed,
                ChangedInventory = input.ChangedInventory,
                CurrentInventory = model.Inventory
            };
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

        protected override Task<Product> MapToEntityAsync(CreateUpdateProductDto createInput)
        {
            var entity = new Product(
                GuidGenerator.Create(),
                CurrentTenant.Id,
                createInput.StoreId,
                createInput.ProductGroupName,
                createInput.ProductDetailId,
                createInput.UniqueName,
                createInput.DisplayName,
                createInput.Overview,
                createInput.InventoryStrategy,
                createInput.InventoryProviderName,
                createInput.IsPublished,
                false,
                createInput.IsHidden,
                createInput.PaymentExpireIn,
                createInput.MediaResources,
                createInput.DisplayOrder);

            createInput.MapExtraPropertiesTo(entity);

            return Task.FromResult(entity);
        }

        protected override Task MapToEntityAsync(CreateUpdateProductDto updateInput, Product entity)
        {
            entity.Update(
                updateInput.StoreId,
                updateInput.ProductGroupName,
                updateInput.ProductDetailId,
                updateInput.UniqueName,
                updateInput.DisplayName,
                updateInput.Overview,
                updateInput.InventoryStrategy,
                updateInput.InventoryProviderName,
                updateInput.IsPublished,
                false,
                updateInput.IsHidden,
                updateInput.PaymentExpireIn,
                updateInput.MediaResources,
                updateInput.DisplayOrder);

            updateInput.MapExtraPropertiesTo(entity);

            return Task.CompletedTask;
        }

        protected virtual Task<ProductSku> MapToProductSkuAsync(CreateProductSkuDto createInput)
        {
            var entity = new ProductSku(
                GuidGenerator.Create(),
                createInput.AttributeOptionIds,
                createInput.Name,
                createInput.Currency,
                createInput.OriginalPrice,
                createInput.Price,
                createInput.OrderMinQuantity,
                createInput.OrderMaxQuantity,
                createInput.PaymentExpireIn,
                createInput.MediaResources,
                createInput.ProductDetailId
            );

            createInput.MapExtraPropertiesTo(entity);

            return Task.FromResult(entity);
        }

        protected virtual Task MapToProductSkuAsync(UpdateProductSkuDto updateInput, ProductSku entity)
        {
            entity.Update(
                updateInput.Name,
                updateInput.Currency,
                updateInput.OriginalPrice,
                updateInput.Price,
                updateInput.OrderMinQuantity,
                updateInput.OrderMaxQuantity,
                updateInput.PaymentExpireIn,
                updateInput.MediaResources,
                updateInput.ProductDetailId
            );

            updateInput.MapExtraPropertiesTo(entity);

            return Task.CompletedTask;
        }

        protected virtual ProductDto SortAttributesAndOptions(ProductDto productDto)
        {
            productDto.ProductAttributes = productDto.ProductAttributes.OrderBy(x => x.DisplayOrder).ToList();

            foreach (var productAttributeDto in productDto.ProductAttributes)
            {
                productAttributeDto.ProductAttributeOptions = productAttributeDto.ProductAttributeOptions
                    .OrderBy(x => x.DisplayOrder).ToList();
            }

            return productDto;
        }
    }
}