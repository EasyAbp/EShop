using System;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.ProductDetails
{
    public class ProductDetailRepository : EfCoreRepository<IProductsDbContext, ProductDetail, Guid>, IProductDetailRepository
    {
        public ProductDetailRepository(IDbContextProvider<IProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}