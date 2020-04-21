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
    public class ProductCategoryRepository : EfCoreRepository<ProductsDbContext, ProductCategory, Guid>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IDbContextProvider<ProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public virtual async Task<List<ProductCategory>> GetListByCategoryId(Guid categoryId, Guid? storeId,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(pc => pc.CategoryId == categoryId && pc.StoreId == storeId)
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<List<ProductCategory>> GetListByProductId(Guid productId, Guid? storeId,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(pc => pc.ProductId == productId && pc.StoreId == storeId)
                .ToListAsync(cancellationToken);
        }
    }
}