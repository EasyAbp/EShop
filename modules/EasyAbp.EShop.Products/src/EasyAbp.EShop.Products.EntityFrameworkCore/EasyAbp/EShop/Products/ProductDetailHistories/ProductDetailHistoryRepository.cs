using System;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.ProductDetailHistories
{
    public class ProductDetailHistoryRepository : EfCoreRepository<ProductsDbContext, ProductDetailHistory, Guid>, IProductDetailHistoryRepository
    {
        public ProductDetailHistoryRepository(IDbContextProvider<ProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}