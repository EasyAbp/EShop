using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.ProductHistories
{
    public interface IProductHistoryRepository : IRepository<ProductHistory, Guid>
    {
        Task<ProductHistory> GetAsync(Guid productId, DateTime modificationTime,
            CancellationToken cancellationToken = default);
    }
}