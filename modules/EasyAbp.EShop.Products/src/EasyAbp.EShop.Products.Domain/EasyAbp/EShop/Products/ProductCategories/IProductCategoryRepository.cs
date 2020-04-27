using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.ProductCategories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory, Guid>
    {
        Task<List<ProductCategory>> GetListByCategoryId(Guid categoryId, Guid storeId, CancellationToken cancellationToken = default);
        
        Task<List<ProductCategory>> GetListByProductId(Guid productId, Guid storeId, CancellationToken cancellationToken = default);
    }
}