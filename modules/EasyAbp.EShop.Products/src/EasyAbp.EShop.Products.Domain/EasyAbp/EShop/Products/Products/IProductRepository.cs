using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<IQueryable<Product>> GetQueryableAsync(Guid categoryId);

        Task<IQueryable<Product>> WithDetailsAsync(Guid categoryId);

        Task CheckUniqueNameAsync(Product entity, CancellationToken cancellationToken = new CancellationToken());
    }
}