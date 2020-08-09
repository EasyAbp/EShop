using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.ProductStores
{
    public class ProductStoreRepository : EfCoreRepository<IProductsDbContext, ProductStore, Guid>, IProductStoreRepository
    {
        public ProductStoreRepository(IDbContextProvider<ProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public virtual async Task<ProductStore> GetAsync(Guid productId, Guid storeId, CancellationToken cancellationToken = default)
        {
            var entity = await GetQueryable().Where(x => x.ProductId == productId && x.StoreId == storeId)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(ProductStore), new {ProductId = productId, StoreId = storeId});
            }

            return entity;
        }
    }
}