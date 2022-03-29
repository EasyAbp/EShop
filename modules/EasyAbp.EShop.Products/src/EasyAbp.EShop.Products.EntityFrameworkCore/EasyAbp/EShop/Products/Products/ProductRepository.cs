using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public override async Task<Product> UpdateAsync(Product entity, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
        {
            await CheckUniqueNameAsync(entity, cancellationToken);

            return await base.UpdateAsync(entity, autoSave, cancellationToken);
        }

        public override async Task<Product> InsertAsync(Product entity, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
        {
            await CheckUniqueNameAsync(entity, cancellationToken);

            return await base.InsertAsync(entity, autoSave, cancellationToken);
        }

        public virtual async Task CheckUniqueNameAsync(Product entity, CancellationToken cancellationToken = new CancellationToken())
        {
            if (entity.UniqueName.IsNullOrEmpty())
            {
                return;
            }
            
            if (await (await GetDbSetAsync()).AnyAsync(
                x => x.StoreId == entity.StoreId && x.UniqueName == entity.UniqueName && x.Id != entity.Id,
                cancellationToken))
            {
                throw new DuplicatedProductUniqueNameException(entity.UniqueName);
            }
        }

        public override async Task<IQueryable<Product>> WithDetailsAsync()
        {
            return (await base.WithDetailsAsync())
                .Include(x => x.ProductAttributes).ThenInclude(x => x.ProductAttributeOptions)
                .Include(x => x.ProductSkus);
        }

        public virtual async Task<IQueryable<Product>> GetQueryableAsync(Guid categoryId)
        {
            return await JoinProductCategoriesAsync(await GetDbSetAsync(), categoryId);
        }

        public virtual async Task<IQueryable<Product>> WithDetailsAsync(Guid categoryId)
        {
            return await JoinProductCategoriesAsync(await WithDetailsAsync(), categoryId);
        }

        protected virtual async Task<IQueryable<Product>> JoinProductCategoriesAsync(IQueryable<Product> queryable, Guid categoryId)
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