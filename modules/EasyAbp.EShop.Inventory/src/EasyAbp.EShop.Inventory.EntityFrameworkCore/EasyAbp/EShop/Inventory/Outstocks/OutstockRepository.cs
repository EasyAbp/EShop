using System;
using EasyAbp.EShop.Inventory.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Inventory.Outstocks
{
    public class OutstockRepository : EfCoreRepository<IInventoryDbContext, Outstock, Guid>, IOutstockRepository
    {
        public OutstockRepository(IDbContextProvider<IInventoryDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}