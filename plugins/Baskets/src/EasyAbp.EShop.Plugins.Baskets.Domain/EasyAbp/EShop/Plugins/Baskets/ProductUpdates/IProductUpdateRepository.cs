using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.Baskets.ProductUpdates
{
    public interface IProductUpdateRepository : IRepository<ProductUpdate, Guid>
    {
        Task<List<ProductUpdate>> GetListByProductSkuIdsAsync(IEnumerable<Guid> productSkuIds, CancellationToken cancellationToken = default);
    }
}