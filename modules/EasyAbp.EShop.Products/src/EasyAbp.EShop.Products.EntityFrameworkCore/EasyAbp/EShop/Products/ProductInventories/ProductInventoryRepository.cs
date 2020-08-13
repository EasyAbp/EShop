using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using EasyAbp.EShop.Products.Products;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public class ProductInventoryRepository : EfCoreRepository<IProductsDbContext, ProductInventory, Guid>, IProductInventoryRepository
    {
        public ProductInventoryRepository(IDbContextProvider<IProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<InventoryDataModel> GetInventoryDataAsync(Guid productSkuId, CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .Where(x => x.ProductSkuId == productSkuId)
                .Select(x => new InventoryDataModel
                {
                    Inventory = x.Inventory,
                    Sold = x.Sold
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Dictionary<Guid, InventoryDataModel>> GetInventoryDataDictionaryAsync(List<Guid> productSkuIds, CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .Where(x => productSkuIds.Contains(x.ProductSkuId))
                .ToDictionaryAsync(x => x.ProductSkuId, x => new InventoryDataModel
                {
                    Inventory = x.Inventory,
                    Sold = x.Sold
                }, cancellationToken);
        }
    }
}