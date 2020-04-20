using System;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.Categories
{
    public class CategoryRepository : EfCoreRepository<ProductsDbContext, Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(IDbContextProvider<ProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}