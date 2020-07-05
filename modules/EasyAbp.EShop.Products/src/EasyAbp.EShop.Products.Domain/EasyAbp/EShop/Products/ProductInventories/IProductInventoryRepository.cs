using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public interface IProductInventoryRepository : IRepository<ProductInventory, Guid>
    {
        Task<int> GetInventoryAsync(Guid productSkuId, CancellationToken cancellationToken = default);
        
        Task<Dictionary<Guid, int>> GetInventoryDictionaryAsync(List<Guid> productSkuIds, CancellationToken cancellationToken = default);
    }
}