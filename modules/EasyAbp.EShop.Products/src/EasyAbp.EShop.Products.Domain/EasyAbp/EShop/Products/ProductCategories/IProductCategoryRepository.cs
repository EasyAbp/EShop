using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.ProductCategories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory, Guid>
    {
        Task<List<ProductCategory>> GetListByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
        
        Task<List<ProductCategory>> GetListByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}