using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.ProductStores;
using EasyAbp.EShop.Stores.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Options;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAppService : CrudAppService<Product, ProductDto, Guid, GetProductListDto, CreateUpdateProductDto
            , CreateUpdateProductDto>,
        IProductAppService
    {
        protected override string CreatePolicyName { get; set; } = ProductsPermissions.Products.Create;
        protected override string DeletePolicyName { get; set; } = ProductsPermissions.Products.Delete;
        protected override string UpdatePolicyName { get; set; } = ProductsPermissions.Products.Update;
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = null;

        private readonly IProductManager _productManager;
        private readonly EShopProductsOptions _options;
        private readonly IProductInventoryProvider _productInventoryProvider;
        private readonly IAttributeOptionIdsSerializer _attributeOptionIdsSerializer;
        private readonly IProductStoreRepository _productStoreRepository;
        private readonly IProductRepository _repository;

        public ProductAppService(
            IProductManager productManager,
            IOptions<EShopProductsOptions> options,
            IProductInventoryProvider productInventoryProvider,
            IAttributeOptionIdsSerializer attributeOptionIdsSerializer,
            IProductStoreRepository productStoreRepository,
            IProductRepository repository) : base(repository)
        {
            _productManager = productManager;
            _options = options.Value;
            _productInventoryProvider = productInventoryProvider;
            _attributeOptionIdsSerializer = attributeOptionIdsSerializer;
            _productStoreRepository = productStoreRepository;
            _repository = repository;
        }

        protected override IQueryable<Product> CreateFilteredQuery(GetProductListDto input)
        {
            var query = _repository.WithDetails(input.StoreId, input.CategoryId);

            return input.ShowHidden ? query : query.Where(x => !x.IsHidden);
        }

        protected override Product MapToEntity(CreateUpdateProductDto createInput)
        {
            var product = base.MapToEntity(createInput);

            return product;
        }

        public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            await AuthorizationService.CheckMultiStorePolicyAsync(input.StoreId, CreatePolicyName,
                ProductsPermissions.Products.CrossStore);

            var product = MapToEntity(input);

            TryToSetTenantId(product);

            await UpdateProductAttributesAsync(product, input);

            await _productManager.CreateAsync(product, input.StoreId, input.CategoryIds);

            var dto = MapToGetOutputDto(product);
            
            await LoadDtoExtraDataAsync(product, dto, input.StoreId);
            await LoadDtosProductGroupDisplayNameAsync(new[] {dto});

            return dto;
        }

        public override async Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            await AuthorizationService.CheckMultiStorePolicyAsync(input.StoreId, UpdatePolicyName,
                ProductsPermissions.Products.CrossStore);

            await CheckStoreIsProductOwnerAsync(id, input.StoreId);

            var product = await GetEntityByIdAsync(id);

            CheckProductIsNotStatic(product);

            MapToEntity(input, product);

            await UpdateProductAttributesAsync(product, input);

            await _productManager.UpdateAsync(product, input.CategoryIds);

            var dto = MapToGetOutputDto(product);
            
            await LoadDtoExtraDataAsync(product, dto, input.StoreId);
            await LoadDtosProductGroupDisplayNameAsync(new[] {dto});

            return dto;
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
                        attributeDto.DisplayName, attributeDto.Description);

                    product.ProductAttributes.Add(attribute);
                }

                foreach (var optionDto in attributeDto.ProductAttributeOptions)
                {
                    var option =
                        attribute.ProductAttributeOptions.FirstOrDefault(o => o.DisplayName == optionDto.DisplayName);

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

            await LoadDtoExtraDataAsync(product, dto, storeId);
            await LoadDtosProductGroupDisplayNameAsync(new[] {dto});

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

        public virtual async Task<ProductDto> GetByCodeAsync(string code, Guid storeId)
        {
            await CheckGetPolicyAsync();

            var product = await _repository.GetAsync(x => x.UniqueName == code);

            if (!product.IsPublished)
            {
                await CheckStoreIsProductOwnerAsync(product.Id, storeId);
            }

            var dto = MapToGetOutputDto(product);

            await LoadDtoExtraDataAsync(product, dto, storeId);
            await LoadDtosProductGroupDisplayNameAsync(new[] {dto});

            return dto;
        }

        public override async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input)
        {
            await CheckGetListPolicyAsync();

            var isCurrentUserStoreAdmin =
                await AuthorizationService.IsMultiStoreGrantedAsync(input.StoreId,
                    ProductsPermissions.Products.Default, ProductsPermissions.Products.CrossStore);

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

                await LoadDtoExtraDataAsync(product, productDto, input.StoreId);

                items.Add(productDto);
            }

            await LoadDtosProductGroupDisplayNameAsync(items);

            return new PagedResultDto<ProductDto>(totalCount, items);
        }

        protected virtual async Task<ProductDto> LoadDtoInventoryDataAsync(Product product, ProductDto productDto,
            Guid storeId)
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

        protected virtual async Task<ProductDto> LoadDtoExtraDataAsync(Product product, ProductDto productDto, Guid storeId)
        {
            await LoadDtoInventoryDataAsync(product, productDto, storeId);
            await LoadDtoPriceDataAsync(product, productDto, storeId);

            return productDto;
        }
        
        protected virtual async Task<ProductDto> LoadDtoPriceDataAsync(Product product, ProductDto productDto, Guid storeId)
        {
            foreach (var productSku in product.ProductSkus)
            {
                var productSkuDto = productDto.ProductSkus.First(x => x.Id == productSku.Id);

                var priceDataModel = await _productManager.GetProductPriceAsync(product, productSku, storeId);
                
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

        public async Task DeleteAsync(Guid id, Guid storeId)
        {
            await AuthorizationService.CheckMultiStorePolicyAsync(storeId, DeletePolicyName,
                ProductsPermissions.Products.CrossStore);

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
            await AuthorizationService.CheckMultiStorePolicyAsync(storeId, UpdatePolicyName,
                ProductsPermissions.Products.CrossStore);

            await CheckStoreIsProductOwnerAsync(productId, storeId);

            var product = await GetEntityByIdAsync(productId);

            CheckProductIsNotStatic(product);

            var sku = ObjectMapper.Map<CreateProductSkuDto, ProductSku>(input);

            EntityHelper.TrySetId(sku, GuidGenerator.Create);

            await _productManager.CreateSkuAsync(product, sku);

            var dto = MapToGetOutputDto(product);
            
            await LoadDtoExtraDataAsync(product, dto, storeId);
            await LoadDtosProductGroupDisplayNameAsync(new[] {dto});

            return dto;
        }

        public async Task<ProductDto> UpdateSkuAsync(Guid productId, Guid productSkuId, Guid storeId,
            UpdateProductSkuDto input)
        {
            await AuthorizationService.CheckMultiStorePolicyAsync(storeId, UpdatePolicyName,
                ProductsPermissions.Products.CrossStore);

            await CheckStoreIsProductOwnerAsync(productId, storeId);

            var product = await GetEntityByIdAsync(productId);

            CheckProductIsNotStatic(product);

            var sku = product.ProductSkus.Single(x => x.Id == productSkuId);

            ObjectMapper.Map(input, sku);

            await _productManager.UpdateSkuAsync(product, sku);

            var dto = MapToGetOutputDto(product);
            
            await LoadDtoExtraDataAsync(product, dto, storeId);
            await LoadDtosProductGroupDisplayNameAsync(new[] {dto});

            return dto;
        }

        public async Task<ProductDto> DeleteSkuAsync(Guid productId, Guid productSkuId, Guid storeId)
        {
            await AuthorizationService.CheckMultiStorePolicyAsync(storeId, UpdatePolicyName,
                ProductsPermissions.Products.CrossStore);

            await CheckStoreIsProductOwnerAsync(productId, storeId);

            var product = await GetEntityByIdAsync(productId);

            CheckProductIsNotStatic(product);

            var sku = product.ProductSkus.Single(x => x.Id == productSkuId);

            await _productManager.DeleteSkuAsync(product, sku);

            var dto = MapToGetOutputDto(product);
            
            await LoadDtoExtraDataAsync(product, dto, storeId);
            await LoadDtosProductGroupDisplayNameAsync(new[] {dto});

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
    }
}