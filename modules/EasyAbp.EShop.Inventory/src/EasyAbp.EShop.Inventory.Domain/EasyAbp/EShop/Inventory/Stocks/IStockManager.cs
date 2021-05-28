using EasyAbp.EShop.Inventory.Instocks;
using EasyAbp.EShop.Inventory.Outstocks;
using EasyAbp.EShop.Inventory.Reallocations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Inventory.Stocks
{
    public interface IStockManager : IDomainService
    {
        Task<Stock> CreateAsync(Stock stock);

        Task<Stock> UpdateAsync(Stock stock);

        Task DeleteAsync(Stock stock);

        Task DeleteInventoryAsync(Guid id);

        Task<Instock> CreateAsync(Instock instock);

        Task<Instock> UpdateAsync(Instock instock);

        Task DeleteAsync(Instock instock);

        Task DeleteInstockAsync(Guid id);

        Task<Outstock> CreateAsync(Outstock instock);

        Task<Outstock> UpdateAsync(Outstock instock);

        Task DeleteAsync(Outstock instock);

        Task DeleteOutstockAsync(Guid id);

        Task<Reallocation> CreateAsync(Reallocation instock);

        Task<Reallocation> UpdateAsync(Reallocation instock);

        Task DeleteAsync(Reallocation instock);

        Task DeleteReallocationAsync(Guid id);

        Task<Stock> AdjustStock(Guid productSkuId, Guid warehouseId, int adjustedQuantity, string description = null);
    }
}
