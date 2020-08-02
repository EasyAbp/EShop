using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Stores.Stores
{
    public interface IStoreManager : IDomainService
    {
        Task<Store> CreateAsync(Store store, IEnumerable<Guid> ownerIds = null);

        Task<Store> UpdateAsync(Store store, IEnumerable<Guid> ownerIds = null);
    }
}
