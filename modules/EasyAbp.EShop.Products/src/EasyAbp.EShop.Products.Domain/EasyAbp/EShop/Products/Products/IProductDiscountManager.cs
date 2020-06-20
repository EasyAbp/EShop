using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductDiscountManager : IDomainService
    {
        Task<decimal> GetDiscountedPriceAsync(Product product, ProductSku productSku, Guid storeId);
    }
}