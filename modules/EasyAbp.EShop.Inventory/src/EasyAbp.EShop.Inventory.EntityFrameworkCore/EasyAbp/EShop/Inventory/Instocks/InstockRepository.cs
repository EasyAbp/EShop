using System;
using EasyAbp.EShop.Inventory.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Inventory.Instocks
{
    public class InstockRepository : EfCoreRepository<IInventoryDbContext, Instock, Guid>, IInstockRepository
    {
        public InstockRepository(IDbContextProvider<IInventoryDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}