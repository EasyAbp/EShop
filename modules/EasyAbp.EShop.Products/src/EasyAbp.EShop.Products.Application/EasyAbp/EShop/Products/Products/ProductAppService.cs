using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Authorization;
using EasyAbp.EShop.Products.ProductCategories;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.ProductStores;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAppService : CrudAppService<Product, ProductDto, Guid, GetProductListDto, CreateUpdateProductDto, CreateUpdateProductDto>,
        IProductAppService
    {
        protected override string CreatePolicyName { get; set; } = ProductsPermissions.Products.Create;
        protected override string DeletePolicyName { get; set; } = ProductsPermissions.Products.Delete;
        protected override string UpdatePolicyName { get; set; } = ProductsPermissions.Products.Update;
        protected override string GetPolicyName { get; set; } = ProductsPermissions.Products.Default;
        protected override string GetListPolicyName { get; set; } = ProductsPermissions.Products.Default;

        private readonly IProductStoreRepository _productStoreRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _repository;

        public ProductAppService(
            IProductStoreRepository productStoreRepository,
            IProductCategoryRepository productCategoryRepository,
            IProductRepository repository) : base(repository)
        {
            _productStoreRepository = productStoreRepository;
            _productCategoryRepository = productCategoryRepository;
            _repository = repository;
        }

        protected override IQueryable<Product> CreateFilteredQuery(GetProductListDto input)
        {
            return input.CategoryId.HasValue
                ? _repository.GetQueryable(input.StoreId, input.CategoryId.Value)
                : _repository.GetQueryable(input.StoreId);
        }

        public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            await CheckCreatePolicyAsync();

            var product = MapToEntity(input);

            TryToSetTenantId(product);
            
            await UpdateProductAttributesAsync(product, input);

            await Repository.InsertAsync(product, autoSave: true);

            await AddProductToStoreAsync(product.Id, input.StoreId);
            
            await UpdateProductCategoriesAsync(product.Id, input.CategoryIds);

            return MapToGetOutputDto(product);
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
            
            MapToEntity(input, product);

            await UpdateProductAttributesAsync(product, input);

            await Repository.UpdateAsync(product, autoSave: true);

            await UpdateProductCategoriesAsync(product.Id, input.CategoryIds);

            return MapToGetOutputDto(product);
        }

        protected virtual async Task CheckStoreIsProductOwnerAsync(Guid id, Guid storeId)
        {
            var productStore = await _productStoreRepository.GetAsync(id, storeId);

            if (!productStore.IsOwner)
            {
                throw new StoreIsNotProductOwnerException(id, storeId);
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

        [RemoteService(IsMetadataEnabled = false)]
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
        
        public async Task DeleteAsync(Guid id, Guid storeId)
        {
            await _productCategoryRepository.DeleteAsync(x => x.ProductId.Equals(id));
            
            await CheckStoreIsProductOwnerAsync(id, storeId);

            await base.DeleteAsync(id);
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