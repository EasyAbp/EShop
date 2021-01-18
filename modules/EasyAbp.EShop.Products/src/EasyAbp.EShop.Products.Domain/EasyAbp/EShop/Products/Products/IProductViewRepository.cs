using System;
using System.Linq;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductViewRepository : IRepository<ProductView, Guid>
    {
        IQueryable<ProductView> GetQueryable(Guid categoryId);

        IQueryable<ProductView> WithDetails(Guid categoryId);
    }
}