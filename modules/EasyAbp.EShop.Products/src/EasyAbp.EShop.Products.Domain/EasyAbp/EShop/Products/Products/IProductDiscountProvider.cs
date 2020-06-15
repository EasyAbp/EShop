using System;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductDiscountProvider
    {
        Task<decimal> GetDiscountedPriceAsync(Product product, ProductSku productSku, Guid storeId, decimal currentPrice);
    }
}