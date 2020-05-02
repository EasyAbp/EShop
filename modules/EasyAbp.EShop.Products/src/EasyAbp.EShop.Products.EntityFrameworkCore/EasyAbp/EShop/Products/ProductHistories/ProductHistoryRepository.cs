using System;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.ProductHistories
{
    public class ProductHistoryRepository : EfCoreRepository<ProductsDbContext, ProductHistory, Guid>, IProductHistoryRepository
    {
        public ProductHistoryRepository(IDbContextProvider<ProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}