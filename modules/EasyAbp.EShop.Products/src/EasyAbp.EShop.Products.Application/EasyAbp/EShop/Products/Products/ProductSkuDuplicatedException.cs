using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSkuDuplicatedException : BusinessException
    {
        public ProductSkuDuplicatedException(Guid productId, string serializedAttributeOptionIds) : base(
            message: $"Sku {serializedAttributeOptionIds} is duplicate for the product {productId}")
        {
        }
    }
}