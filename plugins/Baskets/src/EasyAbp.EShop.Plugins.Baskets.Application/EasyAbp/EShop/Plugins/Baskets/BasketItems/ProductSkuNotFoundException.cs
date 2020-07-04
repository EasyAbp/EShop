using System;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public class ProductSkuNotFoundException : BusinessException
    {
        public ProductSkuNotFoundException(Guid productId, Guid productSkuId) : base(
            message: $"Product {productId} (SKU: {productSkuId}) not found.")
        {
        }
    }
}