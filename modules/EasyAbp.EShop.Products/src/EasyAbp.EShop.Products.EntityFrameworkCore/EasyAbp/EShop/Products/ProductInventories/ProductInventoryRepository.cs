using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public class ProductInventoryRepository : EfCoreRepository<ProductsDbContext, ProductInventory, Guid>, IProductInventoryRepository
    {
        public ProductInventoryRepository(IDbContextProvider<ProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<int> GetInventoryAsync(Guid productSkuId, CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(x => x.ProductSkuId == productSkuId).Select(x => x.Inventory)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Dictionary<Guid, int>> GetInventoryDictionaryAsync(List<Guid> productSkuIds, CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(x => productSkuIds.Contains(x.ProductSkuId))
                .ToDictionaryAsync(x => x.ProductSkuId, x => x.Inventory, cancellationToken);
        }
    }
}