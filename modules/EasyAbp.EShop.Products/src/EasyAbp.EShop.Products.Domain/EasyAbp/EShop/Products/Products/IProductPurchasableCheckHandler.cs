using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductPurchasableCheckHandler
    {
        Task<CheckProductPurchasableResult> CheckAsync(Product product, ProductSku productSku, Guid storeId,
            Dictionary<string, object> extraProperties);
    }
}