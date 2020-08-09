using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.ProductHistories
{
    public class ProductHistoryRepository : EfCoreRepository<IProductsDbContext, ProductHistory, Guid>, IProductHistoryRepository
    {
        public ProductHistoryRepository(IDbContextProvider<ProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        
        public async Task<ProductHistory> GetAsync(Guid productId, DateTime modificationTime, CancellationToken cancellationToken = default)
        {
            var entity = await GetQueryable()
                .Where(x => x.ModificationTime == modificationTime && x.ProductId == productId)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(ProductHistory), new {productId, modificationTime});
            }

            return entity;
        }
    }
}