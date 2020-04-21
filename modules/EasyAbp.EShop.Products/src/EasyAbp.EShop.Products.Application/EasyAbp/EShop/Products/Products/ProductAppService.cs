using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Authorization;
using EasyAbp.EShop.Products.ProductCategories;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Threading;

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

        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _repository;

        public ProductAppService(
            IProductCategoryRepository productCategoryRepository,
            IProductRepository repository) : base(repository)
        {
            _productCategoryRepository = productCategoryRepository;
            _repository = repository;
        }

        protected override IQueryable<Product> CreateFilteredQuery(GetProductListDto input)
        {
            var query = base.CreateFilteredQuery(input);

            if (input.CategoryId.HasValue)
            {
                var productIds = AsyncHelper
                    .RunSync(() => _productCategoryRepository.GetListByCategoryId(input.CategoryId.Value, input.StoreId))
                    .Select(pc => pc.ProductId).ToList();

                query = query.Where(p => productIds.Contains(p.Id));
            }
            else if (input.StoreId.HasValue)
            {
                query = query.Where(p => p.StoreId == input.StoreId);
            }

            return query;
        }

        public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            await CheckCreatePolicyAsync();

            var product = MapToEntity(input);

            TryToSetTenantId(product);
            
            await UpdateProductAttributesAsync(product, input);

            await Repository.InsertAsync(product, autoSave: true);
            
            await UpdateProductCategoriesAsync(product.Id, product.StoreId, input.CategoryIds);

            return MapToGetOutputDto(product);
        }

        public override async Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            await CheckUpdatePolicyAsync();

            var product = await GetEntityByIdAsync(id);
            
            MapToEntity(input, product);

            await UpdateProductAttributesAsync(product, input);

            await Repository.UpdateAsync(product, autoSave: true);

            await UpdateProductCategoriesAsync(product.Id, product.StoreId, input.CategoryIds);

            return MapToGetOutputDto(product);
        }

        private async Task UpdateProductAttributesAsync(Product product, CreateUpdateProductDto input)
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

        public override async Task DeleteAsync(Guid id)
        {
            await _productCategoryRepository.DeleteAsync(x => x.ProductId.Equals(id));

            await base.DeleteAsync(id);
        }

        public override async Task<ProductDto> GetAsync(Guid id)
        {
            var dto = await base.GetAsync(id);

            dto.CategoryIds = (await _productCategoryRepository.GetListByProductId(dto.Id, dto.StoreId))
                .Select(x => x.CategoryId).ToList();
            
            return dto;
        }

        protected virtual async Task UpdateProductCategoriesAsync(Guid productId, Guid? storeId, IEnumerable<Guid> categoryIds)
        {
            await _productCategoryRepository.DeleteAsync(x => x.ProductId.Equals(productId));

            foreach (var categoryId in categoryIds)
            {
                await _productCategoryRepository.InsertAsync(new ProductCategory(GuidGenerator.Create(),
                    CurrentTenant.Id, storeId, categoryId, productId));
            }
        }
    }
}