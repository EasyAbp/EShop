using System;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductPriceProvider
    {
        Task<decimal> GetPriceAsync(Product product, ProductSku productSku, Guid storeId);
    }
}