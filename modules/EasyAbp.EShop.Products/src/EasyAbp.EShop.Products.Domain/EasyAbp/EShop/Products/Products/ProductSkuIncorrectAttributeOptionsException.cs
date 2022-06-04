using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSkuIncorrectAttributeOptionsException : BusinessException
    {
        public ProductSkuIncorrectAttributeOptionsException(Guid productId, string serializedAttributeOptionIds) : base(ProductsErrorCodes.ProductSkuIncorrectAttributeOptions)
        {
            WithData(nameof(productId), productId);
            WithData(nameof(serializedAttributeOptionIds), serializedAttributeOptionIds);
        }
    }
}