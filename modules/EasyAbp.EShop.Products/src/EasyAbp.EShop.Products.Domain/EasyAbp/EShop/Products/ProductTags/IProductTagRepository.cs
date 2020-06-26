using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.ProductTags
{
    public interface IProductTagRepository : IRepository<ProductTag, Guid>
    {
        Task<List<ProductTag>> GetListByTagIdAsync(Guid tagId, CancellationToken cancellationToken = default);

        Task<List<ProductTag>> GetListByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}
