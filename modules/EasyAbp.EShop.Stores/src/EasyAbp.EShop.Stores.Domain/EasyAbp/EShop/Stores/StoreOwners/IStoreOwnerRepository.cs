using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    public interface IStoreOwnerRepository : IRepository<StoreOwner, Guid>
    {
        Task<List<StoreOwner>> GetListByStoreIdAsync(Guid storeId, CancellationToken cancellationToken = default);

        Task<List<StoreOwner>> GetListByOwnerUserIdAsync(Guid ownerUserId, CancellationToken cancellationToken = default);

        Task<bool> IsExistAsync(Guid storeId, Guid ownerUserId, CancellationToken cancellationToken = default);
    }
}