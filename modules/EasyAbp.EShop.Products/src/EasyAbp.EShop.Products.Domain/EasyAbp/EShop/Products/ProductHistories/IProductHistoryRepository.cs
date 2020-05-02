using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.ProductHistories
{
    public interface IProductHistoryRepository : IRepository<ProductHistory, Guid>
    {
    }
}