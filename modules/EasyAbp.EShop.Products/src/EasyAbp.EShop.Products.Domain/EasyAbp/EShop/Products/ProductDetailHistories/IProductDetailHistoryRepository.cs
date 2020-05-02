using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.ProductDetailHistories
{
    public interface IProductDetailHistoryRepository : IRepository<ProductDetailHistory, Guid>
    {
        Task<ProductDetailHistory> GetAsync(Guid productDetailId, DateTime modificationTime,
            CancellationToken cancellationToken = default);
    }
}