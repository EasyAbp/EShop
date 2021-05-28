using System;
using EasyAbp.EShop.Inventory.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Inventory.StockHistories
{
    public class StockHistoryRepository : EfCoreRepository<IInventoryDbContext, StockHistory, Guid>, IStockHistoryRepository
    {
        public StockHistoryRepository(IDbContextProvider<IInventoryDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}