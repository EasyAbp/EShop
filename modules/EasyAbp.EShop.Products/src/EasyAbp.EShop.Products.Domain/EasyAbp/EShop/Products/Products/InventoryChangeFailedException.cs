using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class InventoryChangeFailedException : BusinessException
    {
        public InventoryChangeFailedException(
            Guid productId, Guid productSkuId,
            int originalInventory, int changedInventory) : base(ProductsErrorCodes.InventoryChangeFailed)
        {
            WithData(nameof(productId), productId);
            WithData(nameof(productSkuId), productSkuId);
            WithData(nameof(originalInventory), originalInventory);
            WithData(nameof(changedInventory), changedInventory);
        }
    }
}