using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Inventory.StockHistories
{
    public interface IStockHistoryRepository : IRepository<StockHistory, Guid>
    {
    }
}