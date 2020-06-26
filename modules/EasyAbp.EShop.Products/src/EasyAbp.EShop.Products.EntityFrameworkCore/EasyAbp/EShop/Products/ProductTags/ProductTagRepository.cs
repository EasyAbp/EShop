using EasyAbp.EShop.Products.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.ProductTags
{
    public class ProductTagRepository : EfCoreRepository<ProductsDbContext, ProductTag, Guid>, IProductTagRepository
    {
        public ProductTagRepository(IDbContextProvider<ProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public virtual async Task<List<ProductTag>> GetListByTagIdAsync(Guid categoryId,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(pc => pc.TagId == categoryId).ToListAsync(cancellationToken);
        }

        public virtual async Task<List<ProductTag>> GetListByProductIdAsync(Guid productId,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(pc => pc.ProductId == productId).ToListAsync(cancellationToken);
        }
    }
}
