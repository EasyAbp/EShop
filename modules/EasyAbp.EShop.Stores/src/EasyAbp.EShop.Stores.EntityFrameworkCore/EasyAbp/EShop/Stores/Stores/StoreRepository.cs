using System;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Stores.Stores
{
    public class StoreRepository : EfCoreRepository<StoresDbContext, Store, Guid>, IStoreRepository
    {
        public StoreRepository(IDbContextProvider<StoresDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Store> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
        {
            return await WithDetails().FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}