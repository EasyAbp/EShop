using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.ProductDetailHistories
{
    public interface IProductDetailHistoryRepository : IRepository<ProductDetailHistory, Guid>
    {
    }
}