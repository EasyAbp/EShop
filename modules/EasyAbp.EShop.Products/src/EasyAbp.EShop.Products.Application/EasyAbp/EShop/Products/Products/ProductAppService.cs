using System;
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

            var entity = MapToEntity(input);

            TryToSetTenantId(entity);

            await Repository.InsertAsync(entity, autoSave: true);

            return MapToGetOutputDto(entity);
        }
    }
}