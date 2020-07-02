using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Authorization;
using EasyAbp.EShop.Products.ProductCategories.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductCategories
{
    public class ProductCategoryAppService : ReadOnlyAppService<ProductCategory, ProductCategoryDto, Guid, GetProductCategoryListDto>,
        IProductCategoryAppService
    {
        protected override string GetListPolicyName { get; set; } = ProductsPermissions.Products.Default;
        
        private readonly IProductCategoryRepository _repository;

        public ProductCategoryAppService(IProductCategoryRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override IQueryable<ProductCategory> CreateFilteredQuery(GetProductCategoryListDto input)
        {
            var queryable = Repository.AsQueryable();
            
            if (input.CategoryId.HasValue)
            {
                queryable = queryable.Where(x => x.CategoryId == input.CategoryId);
            }

            if (input.ProductId.HasValue)
            {
                queryable = queryable.Where(x => x.ProductId == input.ProductId);
            }

            return queryable;
        }

        [RemoteService(false)]
        public override Task<ProductCategoryDto> GetAsync(Guid id)
        {
            throw new NotSupportedException();
        }
    }
}