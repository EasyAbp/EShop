using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSkuDuplicatedException : BusinessException
    {
        public ProductSkuDuplicatedException(Guid productId, string serializedAttributeOptionIds) : base(ProductsErrorCodes.ProductSkuDuplicated)
        {
            WithData(nameof(productId), productId);
            WithData(nameof(serializedAttributeOptionIds), serializedAttributeOptionIds);
        }
    }
}