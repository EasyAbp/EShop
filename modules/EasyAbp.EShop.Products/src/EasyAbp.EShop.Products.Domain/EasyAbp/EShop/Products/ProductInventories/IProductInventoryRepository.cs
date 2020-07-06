using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public interface IProductInventoryRepository : IRepository<ProductInventory, Guid>
    {
        Task<InventoryDataModel> GetInventoryDataAsync(Guid productSkuId, CancellationToken cancellationToken = default);
        
        Task<Dictionary<Guid, InventoryDataModel>> GetInventoryDataDictionaryAsync(List<Guid> productSkuIds, CancellationToken cancellationToken = default);
    }
}