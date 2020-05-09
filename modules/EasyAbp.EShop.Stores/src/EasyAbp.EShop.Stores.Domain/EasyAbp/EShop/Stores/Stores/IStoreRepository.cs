using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Stores.Stores
{
    public interface IStoreRepository : IRepository<Store, Guid>
    {
        Task<Store> FindDefaultStoreAsync(CancellationToken cancellationToken = default);
    }
}