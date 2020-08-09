using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.ProductCategories
{
    public class ProductCategoryRepository : EfCoreRepository<IProductsDbContext, ProductCategory, Guid>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IDbContextProvider<IProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public virtual async Task<List<ProductCategory>> GetListByCategoryIdAsync(Guid categoryId,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(pc => pc.CategoryId == categoryId).ToListAsync(cancellationToken);
        }

        public virtual async Task<List<ProductCategory>> GetListByProductIdAsync(Guid productId,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(pc => pc.ProductId == productId).ToListAsync(cancellationToken);
        }
    }
}