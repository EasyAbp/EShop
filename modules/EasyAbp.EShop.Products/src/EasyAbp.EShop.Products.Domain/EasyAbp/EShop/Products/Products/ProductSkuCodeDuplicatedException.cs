using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSkuCodeDuplicatedException : BusinessException
    {
        public ProductSkuCodeDuplicatedException(Guid productId, string code) : base(ProductsErrorCodes.ProductSkuCodeDuplicated)
        {
            WithData(nameof(productId), productId);
            WithData(nameof(code), code);
        }
    }
}