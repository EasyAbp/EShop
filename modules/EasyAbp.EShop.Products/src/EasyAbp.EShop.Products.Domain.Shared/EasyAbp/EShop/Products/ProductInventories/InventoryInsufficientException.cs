using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public class InventoryInsufficientException : BusinessException
    {
        public InventoryInsufficientException(Guid productId, Guid productSkuId, int quantity, int inventory) : base(
            message: $"The inventory of the product {productId} (SKU: {productSkuId}) is insufficient, {quantity} are needed, but only {inventory}.")
        {
        }
    }
}