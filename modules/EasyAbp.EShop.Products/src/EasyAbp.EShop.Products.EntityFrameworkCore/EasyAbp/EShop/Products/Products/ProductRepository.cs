using System;
using System.Linq;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductRepository : EfCoreRepository<ProductsDbContext, Product, Guid>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<ProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override IQueryable<Product> WithDetails()
        {
            return base.WithDetails()
                .Include(x => x.ProductAttributes).ThenInclude(x => x.ProductAttributeOptions)
                .Include(x => x.ProductSkus);
        }

        public IQueryable<Product> GetQueryable(Guid storeId, Guid categoryId)
        {
            return JoinProductCategories(GetQueryable(storeId), categoryId);
        }
        
        public IQueryable<Product> GetQueryable(Guid storeId)
        {
            return JoinProductStores(GetQueryable(), storeId);
        }
        
        public IQueryable<Product> WithDetails(Guid storeId, Guid categoryId)
        {
            return JoinProductCategories(WithDetails(storeId), categoryId);
        }
        
        public IQueryable<Product> WithDetails(Guid storeId)
        {
            return JoinProductStores(WithDetails(), storeId);
        }

        protected virtual IQueryable<Product> JoinProductStores(IQueryable<Product> queryable, Guid storeId)
        {
            return queryable.Join(
                DbContext.ProductStores.Where(productStore => productStore.StoreId == storeId),
                product => product.Id,
                productStore => productStore.ProductId,
                (product, productStore) => product
            );
        }
        
        protected virtual IQueryable<Product> JoinProductCategories(IQueryable<Product> queryable, Guid categoryId)
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