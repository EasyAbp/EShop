using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.ProductStores
{
    public interface IProductStoreRepository : IRepository<ProductStore, Guid>
    {
        Task<ProductStore> GetAsync(Guid productId, Guid storeId, CancellationToken cancellationToken = default);
    }
}