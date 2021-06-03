using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.Abp.Trees;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.Categories
{
    public class CategoryRepository : EfCoreTreeRepository<IProductsDbContext, Category>, ICategoryRepository
    {
        public CategoryRepository(IDbContextProvider<IProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override Task InsertManyAsync(IEnumerable<Category> entities, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotSupportedException();
        }

        public override Task UpdateManyAsync(IEnumerable<Category> entities, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotSupportedException();
        }
    }
}