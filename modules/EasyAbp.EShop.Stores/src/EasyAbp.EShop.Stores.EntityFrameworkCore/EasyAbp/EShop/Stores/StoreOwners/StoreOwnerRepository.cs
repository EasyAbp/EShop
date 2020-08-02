using EasyAbp.EShop.Stores.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    public class StoreOwnerRepository : EfCoreRepository<StoresDbContext, StoreOwner, Guid>, IStoreOwnerRepository
    {
        public StoreOwnerRepository(IDbContextProvider<StoresDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<StoreOwner>> GetListByStoreIdAsync(Guid storeId, CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(pc => pc.StoreId == storeId).ToListAsync(cancellationToken);
        }

        public async Task<List<StoreOwner>> GetListByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(pc => pc.OwnerId == ownerId).ToListAsync(cancellationToken);
        }
    }
}