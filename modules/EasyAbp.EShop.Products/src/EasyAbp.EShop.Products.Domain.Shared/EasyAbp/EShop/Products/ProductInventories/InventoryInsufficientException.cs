using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public class InventoryInsufficientException : BusinessException
    {
        public InventoryInsufficientException(Guid productId, Guid productSkuId, int quantity, int inventory) : base(ProductsErrorCodes.InventoryInsufficient)
        {
            WithData(nameof(productId), productId);
            WithData(nameof(productSkuId), productSkuId);
            WithData(nameof(quantity), quantity);
            WithData(nameof(inventory), inventory);
        }
    }
}