using System;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductPurchasableStatusProvider
    {
        Task CheckPurchasableAsync(Product product, ProductSku productSku, Guid storeId);

        Task<GetProductPurchasableStatusResult> GetPurchasableStatusAsync(Product product, ProductSku productSku,
            Guid storeId);
    }
}