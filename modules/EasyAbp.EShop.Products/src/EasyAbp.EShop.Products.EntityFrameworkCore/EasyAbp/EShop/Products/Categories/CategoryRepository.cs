using System;
using EasyAbp.Abp.Trees;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.Categories
{
    public class CategoryRepository : EfCoreTreeRepository<ProductsDbContext, Category>, ICategoryRepository
    {
        public CategoryRepository(IDbContextProvider<ProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}