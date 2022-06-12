using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.ProductCategories.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductCategories
{
    public class ProductCategoryAppService : ReadOnlyAppService<ProductCategory, ProductCategoryDto, Guid, GetProductCategoryListDto>,
        IProductCategoryAppService
    {
        protected override string GetListPolicyName { get; set; } = ProductsPermissions.Products.Manage;
        
        private readonly IProductCategoryRepository _repository;

        public ProductCategoryAppService(IProductCategoryRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override async Task<IQueryable<ProductCategory>> CreateFilteredQueryAsync(GetProductCategoryListDto input)
        {
            var queryable = await Repository.GetQueryableAsync();
            
            if (input.CategoryId.HasValue)
            {
                queryable = queryable.Where(x => x.CategoryId == input.CategoryId);
            }

            if (input.ProductId.HasValue)
            {
                queryable = queryable.Where(x => x.ProductId == input.ProductId);
            }

            queryable = queryable.OrderBy(x => x.DisplayOrder);

            return queryable;
        }

        [RemoteService(false)]
        public override Task<ProductCategoryDto> GetAsync(Guid id)
        {
            throw new NotSupportedException();
        }
    }
}