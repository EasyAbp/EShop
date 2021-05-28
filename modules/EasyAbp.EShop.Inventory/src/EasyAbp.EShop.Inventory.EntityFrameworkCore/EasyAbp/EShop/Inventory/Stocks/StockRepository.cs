using System;
using EasyAbp.EShop.Inventory.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Inventory.Stocks
{
    public class StockRepository : EfCoreRepository<IInventoryDbContext, Stock, Guid>, IStockRepository
    {
        public StockRepository(IDbContextProvider<IInventoryDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}