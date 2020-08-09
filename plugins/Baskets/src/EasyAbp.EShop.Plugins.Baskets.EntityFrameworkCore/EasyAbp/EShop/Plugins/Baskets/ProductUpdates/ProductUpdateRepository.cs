using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Baskets.ProductUpdates
{
    public class ProductUpdateRepository : EfCoreRepository<IBasketsDbContext, ProductUpdate, Guid>, IProductUpdateRepository
    {
        public ProductUpdateRepository(IDbContextProvider<BasketsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<ProductUpdate>> GetListByProductSkuIdsAsync(IEnumerable<Guid> productSkuIds, CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(x => productSkuIds.Contains(x.ProductSkuId))
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}