using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class InventoryChangeFailedException : BusinessException
    {
        public InventoryChangeFailedException(Guid productId, Guid productSkuId, int originalInventory,
            int changedInventory) : base(
            message:
            $"Inventory of product {productId} (SKU: {productSkuId}) cannot be changed by {changedInventory} from {originalInventory}")
        {
        }
    }
}