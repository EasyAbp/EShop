using System;
using System.Linq;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        IQueryable<Product> GetQueryable(Guid storeId, Guid? categoryId = default, Guid? tagId = default);

        IQueryable<Product> WithDetails(Guid storeId, Guid? categoryId = default, Guid? tagId = default);
    }
}