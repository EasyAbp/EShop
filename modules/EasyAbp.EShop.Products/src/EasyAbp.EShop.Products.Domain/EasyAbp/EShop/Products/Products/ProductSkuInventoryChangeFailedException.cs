using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSkuInventoryChangeFailedException : BusinessException
    {
        public ProductSkuInventoryChangeFailedException(Guid productId, Guid productSkuId, int originalInventory,
            int changedInventory) : base(
            message:
            $"Inventory of product {productId} (SKU: {productSkuId}) cannot be changed by {changedInventory} from {originalInventory}")
        {
        }
    }
}