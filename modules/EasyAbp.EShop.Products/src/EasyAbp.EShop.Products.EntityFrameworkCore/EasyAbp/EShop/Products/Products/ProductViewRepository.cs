using System;
using System.Linq;
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

        public override IQueryable<ProductView> WithDetails()
        {
            return base.GetQueryable().IncludeDetails();
        }

        public IQueryable<ProductView> GetQueryable(Guid categoryId)
        {
            return JoinProductCategories(DbSet, categoryId);
        }

        public IQueryable<ProductView> WithDetails(Guid categoryId)
        {
            return JoinProductCategories(WithDetails(), categoryId);
        }
        
        protected virtual IQueryable<ProductView> JoinProductCategories(IQueryable<ProductView> queryable, Guid categoryId)
        {
            return queryable.Join(
                DbContext.ProductCategories.Where(productCategory => productCategory.CategoryId == categoryId),
                product => product.Id,
                productCategory => productCategory.ProductId,
                (product, productCategory) => product
            );
        }
    }
}