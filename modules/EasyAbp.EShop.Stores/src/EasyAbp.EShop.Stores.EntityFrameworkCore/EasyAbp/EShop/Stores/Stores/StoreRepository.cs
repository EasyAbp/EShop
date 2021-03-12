using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Stores.Stores
{
    public class StoreRepository : EfCoreRepository<IStoresDbContext, Store, Guid>, IStoreRepository
    {
        public StoreRepository(IDbContextProvider<IStoresDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Store> FindDefaultStoreAsync(CancellationToken cancellationToken = default)
        {
            return await (await WithDetailsAsync()).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<IQueryable<Store>> GetQueryableOnlyOwnStoreAsync(Guid userId)
        {
            return (await GetDbSetAsync()).Join(
                (await GetDbContextAsync()).StoreOwners.Where(storeOwner => storeOwner.OwnerUserId == userId),
                store => store.Id,
                storeOwner => storeOwner.StoreId,
                (store, storeOwner) => store
            );
        }
    }
}