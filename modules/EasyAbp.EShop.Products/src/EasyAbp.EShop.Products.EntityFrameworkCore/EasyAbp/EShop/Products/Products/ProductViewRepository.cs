using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductViewRepository : EfCoreRepository<IProductsDbContext, ProductView, Guid>, IProductViewRepository
    {
        public ProductViewRepository(IDbContextProvider<IProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<ProductView>> WithDetailsAsync()
        {
            return (await base.GetQueryableAsync()).IncludeDetails();
        }

        public virtual async Task<IQueryable<ProductView>> GetQueryableAsync(Guid categoryId)
        {
            return await JoinProductCategoriesAsync(await GetDbSetAsync(), categoryId);
        }

        public virtual async Task<IQueryable<ProductView>> WithDetailsAsync(Guid categoryId)
        {
            return await JoinProductCategoriesAsync(await WithDetailsAsync(), categoryId);
        }
        
        protected virtual async Task<IQueryable<ProductView>> JoinProductCategoriesAsync(IQueryable<ProductView> queryable, Guid categoryId)
        {
            return queryable.Join(
                (await GetDbContextAsync()).ProductCategories.Where(productCategory => productCategory.CategoryId == categoryId),
                product => product.Id,
                productCategory => productCategory.ProductId,
                (product, productCategory) => product
            );
        }
    }
}