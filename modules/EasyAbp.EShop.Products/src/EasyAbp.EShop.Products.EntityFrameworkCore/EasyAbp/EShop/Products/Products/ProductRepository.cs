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
                .Include(x => x.ProductDetail)
                .Include(x => x.ProductAttributes).ThenInclude(x => x.ProductAttributeOptions)
                .Include(x => x.ProductSkus);
        }

        public IQueryable<Product> GetQueryable(Guid storeId, Guid categoryId)
        {
            return from product in DbContext.Products
                join productStore in DbContext.ProductStores on product.Id equals productStore.ProductId
                join productCategory in DbContext.ProductCategories on product.Id equals productCategory.ProductId
                where productStore.StoreId == storeId && productCategory.CategoryId == categoryId
                select product;
        }
        
        public IQueryable<Product> GetQueryable(Guid storeId)
        {
            return from product in DbContext.Products
                join productStore in DbContext.ProductStores on product.Id equals productStore.ProductId
                where productStore.StoreId == storeId
                select product;
        }
    }
}