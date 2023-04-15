using System;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products;

public interface IProductDiscountResolver
{
    Task<DiscountForProductModels> ResolveAsync(IProduct product, IProductSku productSku,
        decimal priceFromPriceProvider, DateTime now);
}