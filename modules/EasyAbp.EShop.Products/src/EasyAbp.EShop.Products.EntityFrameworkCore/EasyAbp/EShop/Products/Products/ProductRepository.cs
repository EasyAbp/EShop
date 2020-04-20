using System;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductRepository : EfCoreRepository<ProductsDbContext, Product, Guid>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<ProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}