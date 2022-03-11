using System;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public class ProductSkuNotFoundException : BusinessException
    {
        public ProductSkuNotFoundException(Guid productId, Guid productSkuId) : base(BasketsErrorCodes.ProductSkuNotFound)
        {
            WithData(nameof(productId), productId);
            WithData(nameof(productSkuId), productSkuId);
        }
    }
}