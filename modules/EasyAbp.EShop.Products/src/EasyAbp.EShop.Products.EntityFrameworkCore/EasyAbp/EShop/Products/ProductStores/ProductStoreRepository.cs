using EasyAbp.EShop.Products.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.ProductStores
{
    public class ProductStoreRepository : EfCoreRepository<ProductsDbContext, ProductStore, Guid>, IProductStoreRepository
    {
        public ProductStoreRepository(IDbContextProvider<ProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public virtual async Task<ProductStore> GetAsync(Guid productId, Guid storeId, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(x => x.ProductId == productId && x.StoreId == storeId, includeDetails, cancellationToken);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(ProductStore), new { ProductId = productId, StoreId = storeId });
            }

            return entity;
        }
    }
}