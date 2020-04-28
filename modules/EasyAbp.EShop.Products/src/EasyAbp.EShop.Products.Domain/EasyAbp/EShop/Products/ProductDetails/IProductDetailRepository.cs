using System;
using System.Linq;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.ProductDetails
{
    public interface IProductDetailRepository : IRepository<ProductDetail, Guid>
    {
    }
}