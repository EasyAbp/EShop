using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.ProductTypes
{
    public interface IProductTypeRepository : IRepository<ProductType, Guid>
    {
    }
}