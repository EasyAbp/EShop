using System;
using EasyAbp.EShop.Inventory.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Inventory.Reallocations
{
    public class ReallocationRepository : EfCoreRepository<IInventoryDbContext, Reallocation, Guid>, IReallocationRepository
    {
        public ReallocationRepository(IDbContextProvider<IInventoryDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}