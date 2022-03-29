using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductViewRepository : IRepository<ProductView, Guid>
    {
        Task<IQueryable<ProductView>> GetQueryableAsync(Guid categoryId);

        Task<IQueryable<ProductView>> WithDetailsAsync(Guid categoryId);
    }
}