using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.ProductDetailHistories
{
    public class ProductDetailHistoryRepository : EfCoreRepository<IProductsDbContext, ProductDetailHistory, Guid>, IProductDetailHistoryRepository
    {
        public ProductDetailHistoryRepository(IDbContextProvider<IProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<ProductDetailHistory> GetAsync(Guid productDetailId, DateTime modificationTime, CancellationToken cancellationToken = default)
        {
            var entity = await (await GetQueryableAsync())
                .Where(x => x.ModificationTime == modificationTime && x.ProductDetailId == productDetailId)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(ProductDetailHistory), new {productDetailId, modificationTime});
            }

            return entity;
        }
    }
}