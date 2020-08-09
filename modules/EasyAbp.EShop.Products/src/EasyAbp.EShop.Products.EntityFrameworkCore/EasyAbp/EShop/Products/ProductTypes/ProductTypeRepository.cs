using System;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.ProductTypes
{
    public class ProductTypeRepository : EfCoreRepository<IProductsDbContext, ProductType, Guid>, IProductTypeRepository
    {
        public ProductTypeRepository(IDbContextProvider<ProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}