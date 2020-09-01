using System;
using System.Linq;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductRepository : EfCoreRepository<IProductsDbContext, Product, Guid>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<IProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override IQueryable<Product> WithDetails()
        {
            return base.WithDetails()
                .Include(x => x.ProductAttributes).ThenInclude(x => x.ProductAttributeOptions)
                .Include(x => x.ProductSkus);
        }

        public IQueryable<Product> GetQueryable(Guid storeId, Guid? categoryId = null)
        {
            var queryable = GetStoreQueryable(storeId);

            if (categoryId.HasValue)
            {
                queryable = JoinProductCategories(queryable, categoryId.Value);
            }

            return queryable;
        }

        public IQueryable<Product> WithDetails(Guid storeId, Guid? categoryId = null)
        {
            var queryable = WithStoreDetails(storeId);

            if (categoryId.HasValue)
            {
                queryable = JoinProductCategories(queryable, categoryId.Value);
            }

            return queryable;
        }

        protected virtual IQueryable<Product> GetStoreQueryable(Guid storeId)
        {
            return JoinProductStores(GetQueryable(), storeId);
        }

        protected virtual IQueryable<Product> WithStoreDetails(Guid storeId)
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